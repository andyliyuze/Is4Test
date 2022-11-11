
<style scoped>
.main {
  width: 100vw;
  height: 100vh;
  /*display: flex;*/
  position: relative;
}

.out {
  border-radius: 50%;
  background: lightskyblue;
  position: absolute;
}

.in {
  border-radius: 50%;
  background: deepskyblue;
  position: absolute;
}

.plane {
  background: url("../assets/plane.svg");
  width: 64px;
  height: 64px;
  position: absolute;
  /*left: 50%;*/
  /*top: 50%;*/
}
</style>

<template>
  <div
    class="main"
    @touchmove="touchmove"
    @touchstart="touchstart"
    @touchend="touchend"
  >
    <!--两个div，一个是背景，一个是移动时显示的控制小球。虽然在逻辑上有父子关系，
    但是由于使用绝对定位，并不需要在结构上也遵循这个关系-->
    <div class="out" ref="out" v-show="show" :style="outPos"></div>
    <div class="in" ref="in" :style="inPos" v-show="show"></div>

    <div class="plane" ref="plane" :style="planePos"></div>
  </div>
</template>

<script>
export default {
  name: "t3",
  data() {
    return {
      show: false,
      // 大小圆圈的圆心位置
      outX: 0,
      outY: 0,
      inX: 0,
      inY: 0,
      outR: 50,
      inR: 25,
      planeX: 0,
      planeY: 0,
      // 飞机的宽高和速度
      planeSize: 64,
      speed: 10,

      // 方向向量
      moveX: 0,
      moveY: 0,
    };
  },
  computed: {
    outPos() {
      return `width:${this.outR * 2}px;height:${this.outR * 2}px;left:${
        this.outX - this.outR
      }px;top:${this.outY - this.outR}px`;
    },
    inPos() {
      return `width:${this.inR * 2}px;height:${this.inR * 2}px;left:${
        this.inX - this.inR
      }px;top:${this.inY - this.inR}px`;
    },
    planePos() {
      return `left:${this.planeX - this.planeSize / 2}px;top:${
        this.planeY - this.planeSize / 2
      }px`;
    },
  },
  mounted() {
    setInterval(() => {
      this.planeX += this.moveX * this.speed;
      this.planeY += this.moveY * this.speed;
      console.log(this.planeX, this.planeY);
    }, 50);
    this.HEIGHT = document.body.clientHeight;
    this.WIDTH = document.body.clientWidth;
    console.log(this.WIDTH, this.HEIGHT);
    this.planeX = this.WIDTH / 2;
    this.planeY = this.HEIGHT / 2;
  },
  methods: {
    touchstart(e) {
      this.show = true;
      let x = e.touches[0].clientX;
      let y = e.touches[0].clientY;
      this.outX = x;
      this.outY = y;
      this.inX = x;
      this.inY = y;
      this.moveX = 0;
      this.moveY = 0;
    },
    touchend(e) {
      this.show = false;
      this.moveX = 0;
      this.moveY = 0;
    },
    touchmove(e) {
      let x = e.touches[0].clientX;
      let y = e.touches[0].clientY;
      // this.outX = x
      // this.outY = y
      this.inX = x;
      this.inY = y;

      // len是 外面小球与里面小球的最大距离
      // 如果距离过大，调整为两球的半径之和
      let len1 = this.outR;
      let len2 = ((x - this.outX) ** 2 + (y - this.outY) ** 2) ** 0.5;

      // 调整小球的位置，使其不超出大球的范围
      console.log(len1, len2);
      if (len1 < len2) {
        console.log("222222222");
        let dx = this.inX - this.outX;
        let dy = this.inY - this.outY;
        let r = len1 / len2;
        let ndx = dx * r;
        let ndy = dy * r;
        this.inX = this.outX + ndx;
        this.inY = this.outY + ndy;
      }

      // 设置方向向量,y以下为正，x以右为正
      this.moveX = (this.inX - this.outX) / this.outR;
      this.moveY = (this.inY - this.outY) / this.outR;
    },
  },
};
</script>
