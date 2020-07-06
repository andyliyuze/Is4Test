import Vue from 'vue'
import App from './App.vue'
import { routes } from '@/router/router.js'
import VueRouter from 'vue-router';
import axios from 'axios'
import VueAxios from 'vue-axios'
import Mgr from "@/services/SecurityService";

const originalPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err)
}
console.log(routes);
console.log(process.env.VUE_APP_AdminApiURL)

Vue.config.productionTip = false
Vue.use(VueRouter)
Vue.use(VueAxios, axios)
const router = new VueRouter({
  routes // (缩写) 相当于 routes: routes
});
new Vue({
  render: h => h(App),
  router: router
}).$mount('#app')

let mgr = new Mgr();
router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  if (requiresAuth) {
    mgr.getUser().then(a => {
      if (!a) {
        mgr.signIn();
      } else {
        next();
      }
    });
  } else {
    next();
  }
});
