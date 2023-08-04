<template>
  <div class="container">
    <h1 class="text-center">訂單明細</h1>

    <div class="row">
      <div class="col-2">
        <orderlist></orderlist>
      </div>

      <div class="col-10">
        <h2>訂單編號： {{ $route.params.orderId }}</h2>
        <table class="table table-hover">
          <thead>

          </thead>
          <tbody>
            <tr v-for="{ name, price, discountPrice } in product">
              <td>{{ name }}</td>
              <td></td>
              <td></td>
              <td>{{ price }}</td>
              <td>1</td>
              <td></td>
              <td>{{ discountPrice }}</td>
            </tr>
            <tr v-for="{ name, price, type, capacity, discountPrice, deadline } in ticket">
              <td></td>
              <td>{{ name }} </td>
              <td>{{ gettype(type) }} </td>
              <td>{{ price }}</td>
              <td>{{ capacity }}</td>
              <td>{{ deadline }}</td>
              <td>{{ discountPrice }}</td>
            </tr>
          </tbody>
        </table>
        <div class="exit">
          <router-link to="/order" class="btn btn-primary btn-lg">離開</router-link>
        </div>
      </div>

    </div>
  </div>
</template>
    
<script setup>
import orderlist from '../components/orderlist.vue'
import { ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
const route = useRoute();
const router = useRouter();
const ticket = ref([])
const product = ref([])
const loadOrderdetail = async () => {
  const res = await fetch(`https://localhost:7289/api/OrderDetails/${route.params.orderId}`);
  const datas = await res.json();
  //console.log(datas)
  product.value = datas.orderdetailproducts
};
const gettype = (type) => {
  if (type === 0) {
    return '活動'
  }
  else if (type === 1) {
    return '景點'
  }
  else if (type === 2) {
    return '飯店'
  }
  else if (type === 3) {
    return '餐廳'
  }
};
loadOrderdetail()
</script>
    
<style scoped>
.exit {
  position: absolute;
  top: 60%;
  left: 55%;
}
</style>