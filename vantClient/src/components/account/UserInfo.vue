<template>
  <div>
    <van-button v-if="user==null" type="info" @click="toLogin">登录</van-button>
    <van-button v-if="user==null" type="info" @click="weiXinAuthorize">绑定到微信</van-button>
    <div v-else>
      <label>用户名：{{user.profile.name}}</label>
      <div style="height:20px"></div>
      <div>
        <van-button type="info" @click="logout">退出登录</van-button>
      </div>
    </div>
  </div>
</template>
<script>
import { Button } from "vant";
import Mgr from "@/services/SecurityService";
export default {
  components: {
    [Button.name]: Button
  },
  data() {
    return {
      mgr: new Mgr(),
      weixinAuthorizeUrl: "",
      user: {
        profile: {
          name: ""
        }
      }
    };
  },

  methods: {
    logout() {
      this.mgr.signOut();
    },
    toLogin() {
      this.mgr.signIn();
    },
    weiXinAuthorize() {
      document.location.href = this.weixinAuthorizeUrl;
    }
  },
  mounted() {
    this.mgr.getUser().then(user => {
      this.user = user;
    });
    this.mgr.getWeiXinAuthorizeUrl().then(res => {
      this.weixinAuthorizeUrl = res;
    });
  }
};
</script>