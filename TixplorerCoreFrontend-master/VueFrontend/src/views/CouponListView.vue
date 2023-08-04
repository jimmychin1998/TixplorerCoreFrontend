<template>
    <div class="container">
        <h1 class="text-center">我的優惠券</h1>
        <hr>
        <div class="row">
            <div class="col-2">
                <orderlist></orderlist>

            </div>
            <div class="col-10">
                <couponmain :coupon="coupon" :couponlist="couponlist"></couponmain>
            </div>
        </div>
    </div>
</template>
    
<script setup>
import orderlist from '../components/orderlist.vue'
import couponmain from '../components/couponmain.vue';
import { ref } from 'vue'

const Mid = ref('M20010001')
const coupon = ref([]);
const couponlist = ref([])
const loadCoupon = async () => {
    const res = await fetch(`https://localhost:7289/api/CouponLists/${Mid.value}`)
    const datas = await res.json()
    coupon.value = datas.couponsDTO
    couponlist.value = datas.couponsListDTO
    console.log(coupon.value)

}
loadCoupon()
</script>
    
<style scoped></style>