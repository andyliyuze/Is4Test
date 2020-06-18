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
      <van-tabbar-item icon="home-o">标签</van-tabbar-item>
      <van-tabbar-item icon="search">标签</van-tabbar-item>
      <van-tabbar-item icon="friends-o">标签</van-tabbar-item>
      <van-tabbar-item icon="setting-o">标签</van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
import { Button, Tabbar, TabbarItem } from "vant";
export default {
  components: {
    [Button.name]: Button,
    [Tabbar.name]: Tabbar,
    [TabbarItem.name]: TabbarItem
  },
  data() {
    return {
      active: 0
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
    }
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
