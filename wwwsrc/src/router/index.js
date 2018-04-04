import Vue from 'vue'
import Router from 'vue-router'
//@ts-ignore
import Login from '@/components/Login'
//@ts-ignore
import Home from '@/components/Home'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login
    },
    {
      path: '/home',
      name: 'Home',
      component: Home
    }
  ]
})
