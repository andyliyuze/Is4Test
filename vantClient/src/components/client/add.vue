<template>
  <van-form>
    <van-field
      v-model="model.clientId"
      name="客户端Id"
      label="客户端Id"
      placeholder="客户端Id"
      :rules="[{ required: true, message: '客户端Id' }]"
    />
    <van-field v-model="model.clientName" name="名称" label="名称" placeholder="名称" />
    <van-field v-model="model.redirectUri" name="登录重定向Url" label="登录重定向" placeholder="登录重定向Uri" />
    <van-field
      v-model="model.postLogoutRedirectUri"
      name="登出重定向Url"
      label="登出重定向"
      placeholder="登出重定向Uri"
    />
    <van-field name="enabled" label="启用">
      <template #input>
        <van-switch v-model="model.enabled" size="20" />
      </template>
    </van-field>
    <van-field size="small" name="accessTokenType" label="Token类型">
      <template #input>
        <van-radio-group v-model="model.accessTokenType" direction="horizontal">
          <van-radio name="0">Jwt</van-radio>
          <van-radio name="1">Reference</van-radio>
        </van-radio-group>
      </template>
    </van-field>
    <div class="scope_div">
      <span>Scope</span>
      <div>
        <van-cell-group>
          <van-cell v-for="item in allScopes" :key="item.apiName" :title="item.apiName">
            <template #default>
              <van-checkbox-group v-model="model.allowedScopes" direction="horizontal">
                <van-checkbox
                  v-for="item2 in item.scopes"
                  :key="item2"
                  :name="item2"
                  shape="square"
                >{{item2}}</van-checkbox>
              </van-checkbox-group>
            </template>
          </van-cell>
        </van-cell-group>
      </div>
    </div>

    <div style="margin: 16px;">
      <van-button round block type="info" native-type="submit" @click="sumbit">提交</van-button>
    </div>
  </van-form>
</template>
<style>
.scope_div {
  position: relative;
  box-sizing: border-box;
  width: 100%;
  padding: 10px 16px;
  overflow: hidden;
  color: #323233;
  font-size: 14px;
  line-height: 24px;
  background-color: #fff;
}
</style>
<script>
import ClientService from "@/services/ClientService";
import {
  CheckboxGroup,
  Checkbox,
  Field,
  Icon,
  Form,
  Radio,
  Button,
  RadioGroup,
  Search,
  Switch,
  Cell,
  CellGroup,
  Tag
} from "vant";
export default {
  components: {
    [CheckboxGroup.name]: CheckboxGroup,
    [Checkbox.name]: Checkbox,
    [Icon.name]: Icon,
    [Form.name]: Form,
    [Field.name]: Field,
    [Radio.name]: Radio,
    [Button.name]: Button,
    [RadioGroup.name]: RadioGroup,
    [Search.name]: Search,
    [Switch.name]: Switch,
    [Cell.name]: Cell,
    [CellGroup.name]: CellGroup,
    [Tag.name]: Tag
  },
  data() {
    return {
      clientService: new ClientService(),
      allScopes: [],
      model: {
        clientId: "",
        clientName: "",
        accessTokenType: "1",
        allowedScopes: [],
        enabled: false,
        redirectUri: "",
        postLogoutRedirectUri: ""
      }
    };
  },
  methods: {
    sumbit() {}
  },
  mounted() {
    this.clientService.getAllScopes().then(a => (this.allScopes = a));
  }
};
</script>