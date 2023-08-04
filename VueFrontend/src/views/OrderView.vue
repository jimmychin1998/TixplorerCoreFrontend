<template>
        <div class="container">
                <h1 class="text-center">訂單介面</h1>
                <hr>
                <div class="row">
                        <div class="col-2">
                                <orderlist></orderlist>
                        </div>
                        <div class="col-10">
                                <ordertable :order="pageorder" @delete-order="Deleteorder"></ordertable>

                        </div>

                        <div class="col-12 page">
                                <orderpage :totalPage="totalPage" @childclick="childHandler"></orderpage>
                        </div>
                </div>
        </div>
</template>
    
<script setup>
import ordertable from '../components/ordertable.vue';
import orderlist from '../components/orderlist.vue'
import orderpage from '../components/orderpage.vue';
import { ref, computed, defineEmits } from 'vue'
import axios from 'axios';
const thePage = ref(1);

//const Mid = JSON.parse(sessionData);
const Mid = ref('M20010001');
const orders = ref([]);
const pageSize = ref(5);
const emits = defineEmits(['delete-order']);
const loadOrders = async () => {
        const res = await fetch(`https://localhost:7289/api/Orders/${Mid.value}`)
        const datas = await res.json()
        orders.value = datas.orderResult
}
const totalPage = computed(() => Math.ceil(orders.value.length / pageSize.value));
const pageorder = computed(() => {
        const start = (thePage.value - 1) * pageSize.value;
        return orders.value.slice(start, start + pageSize.value);
});
const Deleteorder = async (orderid) => {
        const result = confirm('請問確定要刪除整筆訂單嗎？刪除訂單後將無法回復！！')
        if (result) {
                try {
                        await axios.delete(`https://localhost:7289/api/OrderDetails/${orderid}`);
                        await axios.delete(`https://localhost:7289/api/Orders/${orderid}`);
                        alert('刪除成功')
                        loadOrders()
                } catch (error) {
                        console.log(error)
                }
        }
}

const childHandler = page => {
        thePage.value = page
        loadOrders()
}
loadOrders()
</script>
    
<style scoped>
.page {
        position: relative;
        top: 20%;
}
</style>