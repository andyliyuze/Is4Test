import Vue from 'vue'
import App from './App.vue'
import { routes } from '@/router/router.js'
import VueRouter from 'vue-router';
const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err)
}
console.log(routes);
Vue.config.productionTip = false
Vue.use(VueRouter)
const router = new VueRouter({
  routes // (缩写) 相当于 routes: routes
});
new Vue({
  render: h => h(App),
  router: router
}).$mount('#app')
