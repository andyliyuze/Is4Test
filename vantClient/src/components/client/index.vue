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

      <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="getClients">
        <van-cell v-for="item in list" :key="item.id" :title="item.clientId" />
      </van-list>
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
import { Field, Icon, Popup, Picker, Row, Col, Search, List, Cell } from "vant";
import ClientService from "@/services/ClientService";
import pager from "@/utility/pager";
export default {
  components: {
    [Icon.name]: Icon,
    [Popup.name]: Popup,
    [Field.name]: Field,
    [Picker.name]: Picker,
    [Row.name]: Row,
    [Col.name]: Col,
    [Search.name]: Search,
    [List.name]: List,
    [Cell.name]: Cell
  },
  data() {
    return {
      pager: pager,
      finished: false,
      loading: false,
      clientService: new ClientService(),
      keyword: "",
      value: "城市",
      showPicker: false,
      columns: ["杭州", "宁波", "温州", "嘉兴", "湖州"],
      list: []
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
    },
    async getClients() {
      this.pager.pageIndex++;
      var result = await this.clientService.getClients({
        pageIndex: this.pager.pageIndex,
        pageSize: this.pager.pageSize
      });

      this.list.push(...result.data);

      this.loading = false;
      if (result.hasNextPage) {
        this.finished = false;
      } else {
        this.finished = true;
      }
    }
  }
};
</script>