using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Entities.Request;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeixinController : ControllerBase
    {

        public static readonly string Token = "AndyToken";//与微信小程序后台的Token设置保持一致，区分大小写。
        public static readonly string EncodingAESKey = "AndyAESKey";//与微信小程序后台的EncodingAESKey设置保持一致，区分大小写。
        public static readonly string WxOpenAppId = "wxae9063c950de4ba5";//与微信小程序后台的AppId设置保持一致，区分大小写。
        public static readonly string WxOpenAppSecret = "3165fee88b61a183087eed8ec7567e23";//与微信小程序账号后台的AppId设置保持一致，区分大小写。

        [HttpGet]
        [Route("callback")]
        public string callback()
        {
            var code = this.Request.Query["code"];
            var result = OAuthApi.GetAccessToken(WxOpenAppId, WxOpenAppSecret, code);
            var userInfo = OAuthApi.GetUserInfo(result.access_token, result.openid);
            return $"{userInfo.nickname}:{userInfo.openid}";
        }

        [HttpGet]
        [Route("callback2")]
        public string callback2()
        {
            var re = this.Request;
            return "hello";
        }

        [HttpGet]
        [Route("index")]
        public string index(string echostr)
        {
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
    }
}
