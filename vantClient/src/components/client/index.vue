<template>
  <div class="client_index_div">
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
    <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="getClients">
      <van-cell-group>
        <van-cell />
        <van-cell v-for="item in list" :key="item.id">
          <template #title>
            <span>{{item.clientId}}</span>
            <span style="color:#969799;font-size: 12px;margin-left: 8px;">{{item.accessTokenType}}</span>
          </template>
          <template #label>
            <div style="font-size: 10px;">
              <span>{{item.redirectUris[0]}}</span>
            </div>
          </template>
          <template #default>
            <van-tag
              v-for="item2 in item.allowedScopes"
              :key="item2"
              type="primary"
              size="medium"
            >{{item2}}</van-tag>
          </template>
        </van-cell>
      </van-cell-group>
    </van-list>
    <div class="add_div">
      <van-icon name="add-o" @click="toAddClient" />
    </div>
  </div>
</template>
<style>
.add_div {
  position: fixed;
  width: 100%;
  bottom: 0px;
  height: 50px;
}
.add_div i {
  left: 86%;
  font-size: 34px;

  color: #2196f3;

  z-index: 1500;
  overflow: hidden;
  -webkit-transition-duration: 300ms;
  transition-duration: 300ms;
  /* box-shadow: 0 10px 20px rgba(0, 0, 0, 0.19), 0 6px 6px rgba(0, 0, 0, 0.23); */
  display: -webkit-box;
  display: -ms-flexbox;
  display: -webkit-flex;
}

.van-cell__label,
.van-cell__value .van-tag {
  margin-left: 5px;
}
.van-cell__label {
  line-height: 22px;
}
.client_index_div .van-cell__title {
  font-size: 16px !important;
}
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
import {
  Field,
  Icon,
  Popup,
  Picker,
  Row,
  Col,
  Search,
  List,
  Cell,
  CellGroup,
  Tag
} from "vant";
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
    [Cell.name]: Cell,
    [CellGroup.name]: CellGroup,
    [Tag.name]: Tag
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
        pageSize: 1
      });

      this.list.push(...result.data);

      this.loading = false;
      if (result.hasNextPage) {
        this.finished = false;
      } else {
        this.finished = true;
      }
    },
    toAddClient() {
      this.$router.push("/client/add");
    }
  }
};
</script>