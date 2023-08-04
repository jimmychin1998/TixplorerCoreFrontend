import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterValidView from '@/views/RegisterValidView.vue'
import RegisterView from '@/views/RegisterView.vue'
import RegisterSuccessView from '@/views/RegisterSuccessView.vue'
import ForgetPwdView from '@/views/ForgetPwdView.vue'
import ForgetPwdSuccessView from '@/views/ForgetPwdSuccessView.vue'
import OrderView from '@/views/OrderView.vue'
import ProblemView from '@/views/ProblemView.vue'
import orderdetail from '@/components/orderdetail.vue'
import problemquestionsView from '@/views/problemquestionsView.vue'
import problemorder from '@/components/problemorder.vue'
import problemintegral from '@/components/problemintegral.vue'
import problemregister from '@/components/problemregister.vue'
import problemcard from '@/components/problemcard.vue'
import problemrestaurant from '@/components/problemrestaurant.vue'
import problemmember from '@/components/problemmember.vue'
import problemdiscount from '@/components/problemdiscount.vue'
import problemjourney from '@/components/problemjourney.vue'
import problempolicy from '@/components/problempolicy.vue'
import CouponListView from '@/views/CouponListView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {//首頁
      path: '/',
      name: 'home',
      component: HomeView
    },
    {//登入畫面
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {//註冊畫面 - 第一步：驗證信箱
      path: '/RegisterValid',
      name: 'RegisterValid',
      component: RegisterValidView
    },
    {//註冊畫面 - 第二步：填寫個人資料
      path: '/Register',
      name: 'Register',
      component: RegisterView
    },
    {//註冊畫面 - 第三步：註冊成功
      path: '/RegisterSuccess',
      name: 'RegisterSuccess',
      component: RegisterSuccessView
    },
    {//忘記密碼 → 重設密碼
      path: '/ForgetPwd',
      name: 'ForgetPwd',
      component: ForgetPwdView
    },
    {//忘記密碼 → 重設密碼成功
      path: '/ForgetPwdSuccess',
      name: 'ForgetPwdSuccess',
      component: ForgetPwdSuccessView
    },
    {//訂單畫面
      path: '/order',
      name: 'order',
      component: OrderView
    },
    {//我的優惠券畫面
      path: '/CouponList',
      name: 'CouponList',
      component: CouponListView
    },
    {
      path: '/order/:orderId',
      name: 'orderdetail',
      component: orderdetail
    },
    {
      path: '/problem',
      name: 'problem',
      component: ProblemView
    },
    {
      path: '/problemquestion',
      name: 'problemquestion',
      component: problemquestionsView,
      children: [
        {
          path: '/problemquestion/problemorder',
          component: problemorder,
          name: 'problemorder',
        },
        {
          path: '/problemquestion/problemintegral',
          component: problemintegral,
          name: 'problemintegral',
        },
        {
          path: '/problemquestion/problemregister',
          component: problemregister,
          name: 'problemregister',
        },
        {
          path: '/problemquestion/problemcard',
          component: problemcard,
          name: 'problemcard',
        },
        {
          path: '/problemquestion/problemrestaurant',
          component: problemrestaurant,
          name: 'problemrestaurant',
        },
        {
          path: '/problemquestion/problemmember',
          component: problemmember,
          name: 'problemmember',
        },
        {
          path: '/problemquestion/problemdiscount',
          component: problemdiscount,
          name: 'problemdiscount',
        },
        {
          path: '/problemquestion/problemjourney',
          component: problemjourney,
          name: 'problemjourney',
        },
        {
          path: '/problemquestion/problempolicy',
          component: problempolicy,
          name: 'problempolicy',
        },
      ],
    },
  ]
})

export default router
