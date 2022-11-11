<template>
  <div class="hello">
    <van-button @click="toLogin" type="default">默认按钮</van-button>
    <van-button @click="toRegister" type="primary">主要按钮</van-button>
    <van-button @click="toClient" type="info">信息按钮</van-button>
    <van-button type="warning">警告按钮</van-button>
    <van-button type="danger">危险按钮</van-button>
    <van-button plain hairline type="primary">细边框按钮</van-button>
    <van-button plain hairline type="info">细边框按钮</van-button>
    <van-tabbar v-model="active">
      <van-tabbar-item icon="home-o">首页</van-tabbar-item>
      <van-tabbar-item icon="search">标签</van-tabbar-item>
      <van-tabbar-item icon="friends-o">标签</van-tabbar-item>
      <van-tabbar-item icon="setting-o" @click="toUserInfo">我的</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
import { Button, Tabbar, TabbarItem } from "vant";
import weiXinHelper from "@/utility/weixin.js";
import Mgr from "@/services/SecurityService";
export default {
  components: {
    [Button.name]: Button,
    [Tabbar.name]: Tabbar,
    [TabbarItem.name]: TabbarItem
  },
  data() {
    return {
      active: 0,
      mgr: new Mgr(),
      isWeinBrowser: weiXinHelper.isWeixin()
    };
  },
  name: "HelloWorld",
  props: {
    msg: String
  },
  methods: {
    toLogin() {
      this.$router.push("Login");
    },
    toRegister() {
      this.$router.push("Register");
    },
    toClient() {
      this.$router.push("client/index");
    },
    toUserInfo() {
      this.$router.push("UserInfo");
    }
  },
  mounted() {
    this.mgr.getUser().then(user => {
      console.log(user);
     
      if (this.isWeinBrowser && user == null) {
        this.mgr
          .tryLogin()
          .then(a => {
              window.location.href = a;
          })
          .catch(ex => console.log(ex));
      }
    });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}

ul {
  list-style-type: none;
  padding: 0;
}

li {
  display: inline-block;
  margin: 0 10px;
}

a {
  color: #42b983;
}
</style>
