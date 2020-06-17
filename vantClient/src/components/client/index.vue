<template>
  <div>
    <div class="search_header">
      <van-row>
        <van-col span="2.5">
          <span class="city_div">
            <van-icon size="20" name="wap-home-o" />
          </span>
        </van-col>
        <van-col span="2.5">
          <span style clickable :value="value" @click="showPicker = true">{{value}}</span>
        </van-col>
        <van-col span="8">
          <van-field class="key_div" v-model="key" placeholder="请输入用职位" />
        </van-col>
      </van-row>
      <van-search
        left-icon
        v-model="value"
        show-left-icon
        show-action
        label="城市"
        placeholder="请输入搜索关键词"
        @search="onSearch"
      >
        <template #action>
          <div @click="onSearch">搜索</div>
        </template>
        <template #left-icon></template>
      </van-search>
      <van-popup v-model="showPicker" round position="bottom">
        <van-picker
          show-toolbar
          :columns="columns"
          @cancel="showPicker = false"
          @confirm="onConfirm"
        />
      </van-popup>
    </div>
  </div>
</template>
<style>
.city_div i {
  top: 3px;
  margin-left: 10px;
  margin-right: 5px;
  color: #009688;
}
.key_div.van-cell {
  line-height: 0px;
  padding: 5px 6px;
}
</style>
<script>
import { Field, Icon, Popup, Picker, Row, Col, Search } from "vant";
export default {
  components: {
    [Icon.name]: Icon,
    [Popup.name]: Popup,
    [Field.name]: Field,
    [Picker.name]: Picker,
    [Row.name]: Row,
    [Col.name]: Col,
    [Search.name]: Search
  },
  data() {
    return {
      key: "",
      value: "城市",
      showPicker: false,
      columns: ["杭州", "宁波", "温州", "嘉兴", "湖州"]
    };
  },
  methods: {
    onConfirm(value) {
      this.value = value;
      this.showPicker = false;
    }
  }
};
</script>