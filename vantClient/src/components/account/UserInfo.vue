<template>
  <div>
    <label>用户名：{{user.profile.name}}</label>
    <div style="height:20px"></div>
    <div>
      <van-button type="info" @click="logout">退出登录</van-button>
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

      user: { profile: { name: "" } }
    };
  },

  methods: {
    logout() {
      this.mgr.signOut();
    }
  },
  mounted() {
    this.mgr.getUser().then(user => {
      this.user = user;
    });
  }
};
</script>