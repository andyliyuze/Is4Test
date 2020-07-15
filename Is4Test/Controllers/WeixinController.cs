
using IdentityModel;
using IdentityServer4.Configuration;
using Is4.Domain;
using Is4.Service.Shared;
using Is4Test.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Entities.Request;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Is4Test.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class WeixinController : Controller
    {
        public static readonly string Token = "AndyToken";//与微信小程序后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "AndyAESKey";//与微信小程序后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string WxOpenAppId = "wxae9063c950de4ba5";//与微信小程序后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppSecret = "3165fee88b61a183087eed8ec7567e23";//与微信小程序账号后台的AppId设置保持一致，区分大小写。
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IClientService _clientService;
        private readonly IOptions<IdentityOptions> _options;
        private readonly IOptions<Microsoft.AspNetCore.Authentication.AuthenticationOptions> _identityServerOptions;

        public WeixinController(IConfiguration configuration, UserManager<User> userManager, IUserService userService,
            IClientService clientService, SignInManager<User> signInManager, IOptions<IdentityOptions> options,
            IOptions<Microsoft.AspNetCore.Authentication.AuthenticationOptions> identityServerOptions)
        {
            _configuration = configuration;
            _userManager = userManager;
            _userService = userService;
            _clientService = clientService;
            _signInManager = signInManager;
            _options = options;
            _identityServerOptions = identityServerOptions;
        }


        [HttpGet]
        [Route("getWeiXinAuthorizeUrl")]
        public string GetWeiXinAuthorizeUrl(bool needUserInfo = false)
        {
            var appid = WxOpenAppId;
            var hostUrl = $"{_configuration["HostDomain"]}/api/weixin/callback".UrlEncode();
            var scope = needUserInfo ? "snsapi_userinfo" : "snsapi_base";
            var url = $@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={hostUrl}&response_type=code&scope={scope}&state=123#wechat_redirect";

            return url;
        }


        [HttpGet]
        [Route("tryLogin")]
        public string TryLogin(string clientId, string returnUrl)
        {
            returnUrl = "/connect/authorize" + returnUrl.Split("/connect/authorize")[1];
            this.Response.Cookies.Append("lilili", "lallaalla");
            var appid = WxOpenAppId;
            returnUrl = returnUrl.UrlDecode().UrlEncode();
            var hostUrl = $"{_configuration["HostDomain"]}/api/weixin/loginCallback".UrlEncode();
            hostUrl += $"?returnUrl={returnUrl}";
            var scope = "snsapi_base";
            var url = $@"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appid}&redirect_uri={hostUrl}&response_type=code&scope={scope}&state={clientId}#wechat_redirect";
            url = "http://andyliyuze.oicp.net/api/weixin/loginCallback" + $"?returnUrl={returnUrl}"; ;
            return url;
        }

        [HttpGet]
        [Route("loginCallback")]
        public async Task<IActionResult> LoginCallback()
        {
            var op = "oD4-6t6tF6OBi05z3XfssyWlWB28";
            var ci = "vuejsclient";
            var code = Request.Query["code"];
            var returnUrl = Request.QueryString.Value.Replace("?returnUrl=", "").UrlDecode();
            var state = Request.Query["state"];
           // var result = OAuthApi.GetAccessToken(WxOpenAppId, WxOpenAppSecret, code);
            var user = _userService.GetUserByOpenId(op);
            var f = new UserClaimsPrincipalFactory<User>(_userManager, _options);
            var claims = await f.CreateAsync(user);
 

            await _signInManager.SignInAsync(user, true);

 

            return Redirect(returnUrl);
        }


        [HttpGet]
        [Route("index")]
        public string Index(string echostr)
        {
            var ss = this.Request.Query;
            PostModel postModel = new PostModel()
            {
                Nonce = this.Request.Query["nonce"],
                Signature = this.Request.Query["signature"],
                Timestamp = this.Request.Query["timestamp"],
            };
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, Token))
            {
                return echostr; //返回随机字符串则表示验证通过
            }
            else
            {
                return "Faild";
            }
        }

        [HttpGet]
        [Route("callback")]
        public ActionResult Callback()
        {
            var code = Request.Query["code"];
            var result = OAuthApi.GetAccessToken(WxOpenAppId, WxOpenAppSecret, code);
            var userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);

            return BindUser(userInfo);
        }

        [HttpGet]
        [Route("bindUser")]
        public ActionResult BindUser(OAuthUserInfo oAuthUserInfo)
        {
            var viewModel = new BingWeiXinUserViewModel()
            {
                ReturnUrl = this.Request.GetDisplayUrl(),
                WeiXinHeadUrl = oAuthUserInfo.headimgurl,
                WeiXinOpenId = oAuthUserInfo.openid,
                WeiXinNickName = oAuthUserInfo.nickname,
            };

            return View("BindUser", viewModel);
        }


        [HttpPost]
        [Route("bindUser")]
        public async Task<ActionResult> BindUser(WeiXinUserBindInputModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Json(new { message = "系统无该用户", success = false });
            }
            var pwdCheck = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!pwdCheck)
            {
                return Json(new { message = "密码错误", success = false });
            }
            user.WeiXinOpenId = model.WeiXinOpenId;
            user.WeiXinNickName = model.WeiXinNickName;
            user.WeiXinHeadUrl = model.WeiXinHeadUrl;
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                return Json(new { message = $"绑定成功", success = true });
            }
            else
            {
                return Json(new { message = $"绑定失败:{string.Join(",", updateResult.Errors)}", success = false });
            }
        }
    }
}
