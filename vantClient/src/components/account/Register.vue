<template>
  <div>
    <van-form @submit="onSubmit">
      <van-field
        v-model="username"
        name="username"
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

      <van-field name="head" label="文件上传">
        <template #input>
          <van-uploaderc result-type="file" v-model="fileList" :max-count="1" />
        </template>
      </van-field>
      <div style="margin: 16px;">
        <van-button round block type="info" native-type="submit">提交</van-button>
      </div>
    </van-form>
  </div>
</template>
<script>
import { Form, Button, Field, Uploader } from "vant";
import UserService from "@/services/userService";
export default {
  components: {
    [Uploader.name]: Uploader,
    [Form.name]: Form,
    [Button.name]: Button,
    [Field.name]: Field
  },
  data() {
    return {
      username: "",
      password: "",
      fileList: [
        // Uploader 根据文件后缀来判断是否为图片文件
        // 如果图片 URL 中不包含类型信息，可以添加 isImage 标记来声明
        // { url: "https://cloud-image", isImage: true }
      ],
      userService: new UserService()
    };
  },
  methods: {
    async onSubmit(values) {
      await this.userService.createUser(values);
    }
  }
};
</script>