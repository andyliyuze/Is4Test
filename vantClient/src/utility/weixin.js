const weixinHelper = {
    isWeixin: function () {
        var ua = navigator.userAgent.toLowerCase();

        var isWeixin = ua.indexOf('micromessenger') != -1;

        if (isWeixin) {
            return true;
        } else {
            return false;
        }

    }
}
export default weixinHelper 