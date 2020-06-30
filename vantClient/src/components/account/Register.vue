<template>
  <div>
    <van-form @submit="onSubmit">
      <van-field
        v-model="username"
        name="UserName"
        label="用户名"
        placeholder="用户名"
        :rules="[{ required: true, message: '请填写用户名' }]"
      />
      <van-field
        v-model="password"
        type="password"
        name="password"
        label="密码"
        placeholder="密码"
        :rules="[{ required: true, message: '请填写密码' }]"
      />

      <van-field name="head" label="头像上传">
        <template #input>
          <!-- capture="capture" 直接调用摄像头-->
          <van-uploader image-fit v-model="fileList" :max-count="1" />
        </template>
      </van-field>
      <div style="margin: 16px;">
        <van-button round block type="info" native-type="submit">提交</van-button>
      </div>
    </van-form>
  </div>
</template>
     
<script>
import { Form, Button, Field, Uploader, Toast } from "vant";
import UserService from "@/services/userService";
export default {
  components: {
    [Uploader.name]: Uploader,
    [Form.name]: Form,
    [Button.name]: Button,
    [Field.name]: Field,
    [Toast.name]: Toast
  },
  data() {
    return {
      username: "",
      password: "",
      fileList: [],
      userService: new UserService()
    };
  },
  methods: {
    async onSubmit(values) {
      values.head = values.head[0].content;
      var result = await this.userService.createUser(values);
      console.log(result);
      if (result.result) {
        Toast("注册成功，赶紧去登陆吧");
        this.$router.push({ path: "HelloWorld" });
      } else {
        Toast(result.message);
      }
    }
  }
};
</script>