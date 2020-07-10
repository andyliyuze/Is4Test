export const routes = [
    { path: '/Login', name: "Login", title: "登陆", component: () => import('@/components/account/Login.vue') },
    { path: '/Register', name: "Register", title: "注册", component: () => import('@/components/account/Register.vue') },
    { path: '/HelloWorld', name: "HelloWorld", title: "登陆", component: () => import('@/components/HelloWorld.vue') },
    { path: '/client/index', name: "client_index", title: "客户端", component: () => import('@/components/client/index.vue') },
    { path: '/client/add', name: "AddClient", title: "添加", component: () => import('@/components/client/add.vue') },
    {
        path: '/UserInfo', name: "UserInfo", title: "个人中心", component: () => import('@/components/account/UserInfo.vue'), meta: {
            requiresAuth: false
        }
    }
]