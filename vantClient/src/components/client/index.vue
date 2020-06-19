<template>
  <div>
    <div class="search_header">
      <van-row>
        <van-col span="2.5" style="position: relative;top: 3px;margin-left: 10px;">
          <span class="city_div">
            <van-icon size="20" name="wap-home-o" />
          </span>
        </van-col>
        <van-col span="2.5" style="position: relative;top: 6px;">
          <span style clickable :value="value" @click="showPicker = true">{{value}}</span>
        </van-col>
        <van-col>
          <van-search v-model="keyword" show-action placeholder="请输入搜索关键词">
            <template #action>
              <div @click="onSearch">搜索</div>
            </template>
          </van-search>
        </van-col>
      </van-row>

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
.van-search__content {
  border-radius: 17px;
}
.van-search {
  padding: 0px 2px;
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
      keyword: "",
      value: "城市",
      showPicker: false,
      columns: ["杭州", "宁波", "温州", "嘉兴", "湖州"]
    };
  },
  methods: {
    onConfirm(value) {
      this.value = value;
      this.showPicker = false;
    },
    onSearch() {
      console.log(222);
    },
    onSearch2() {
      console.log(222);
    }
  }
};
</script>