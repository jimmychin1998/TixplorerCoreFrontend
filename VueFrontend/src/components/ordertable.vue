<template>

  <div class="d-flex justify-center">
    <v-locale-provider locale="zh">
      <v-date-picker show-adjacent-months
        color="primary"
      ></v-date-picker>
    </v-locale-provider>
  </div>

  
  <div class="cute-button-group mt-3">
    <a href="#" class="btn btn-primary " :class="{ active: displayOrder === 'all' }"
      @click="displayOrder = 'all'">所有訂單</a>
    <a href="#" class="btn btn-primary" :class="{ active: displayOrder === 'unpaid' }"
      @click="displayOrder = 'unpaid'">未付款</a>
    <a href="#" class="btn btn-primary" :class="{ active: displayOrder === 'history' }"
      @click="displayOrder = 'history'">歷史訂單</a>
  </div>
  <table class="cute-table">
    <thead>
      <tr>
        <th>訂單編號</th>
        <th>訂單金額</th>
        <th>訂單日期</th>
        <th>結單日期</th>
        <th>訂單狀態</th>
        <th>訂單明細</th>
        <th>刪除訂單</th>
        <th>前往付款</th>
      </tr>
    </thead>
    <tbody v-for="item in filteredOrder" :key="item.orderId">
      <tr>
        <td>{{ item.orderId }}</td>
        <td>{{ item.totalprice }}</td>
        <td>{{ formatDate(item.orderdate) }}</td>
        <td>{{ formatDate(item.orderdateEnd) }}</td>
        <td>{{ getOrderStatus(item.state) }}</td>
        <td>
          <i class="bi bi-search" @click="opendetail(item.orderId)"></i>
        </td>
        <td>
          <i class="bi bi-trash" @click="Deleteorder(item.orderId)" v-if="item.state === 0"></i>
        </td>
        <td>
          <i class="bi bi-cart2" v-if="item.state === 0"></i>
        </td>
      </tr>
      <tr v-show="data.isopen && selectedOrderId === item.orderId">
        <th style="background-color: antiquewhite;">套券照片</th>
        <th style="background-color: antiquewhite;">套券名稱</th>
        <th style="background-color: antiquewhite;">票券類型</th>
        <th style="background-color: antiquewhite;">票券使用人數</th>
        <th style="background-color: antiquewhite;">使用期限</th>
      </tr>
      <tr v-show="data.isopen && selectedOrderId === item.orderId"
        v-for="{ image, name, deadline, type, capacity } in combinedData">
        <td><img :src="getimageURL(image)"></td>
        <td>{{ name }}</td>
        <td>{{ gettype(type) }}</td>
        <td>{{ capacity }}</td>
        <td>{{ formatDate(deadline) }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script setup>
import { ref, computed, defineProps, watch, defineEmits, reactive } from 'vue'

const props = defineProps({
  order: Array
});
const thePage = ref(1);
const pageSize = ref(5);
const displayOrder = ref('all');
const selectedOrderId = ref(null);
const product = ref([])
const ticket = ref([])
const combinedData = ref([]);
const emit = defineEmits(['delete-order']);
const data = reactive({
  isopen: false
});
const opendetail = (orderId) => {
  data.isopen = !data.isopen;
  if (data.isopen) {
    selectedOrderId.value = orderId;
    loadOrderdetail(orderId);
  }
};
const Deleteorder = (orderid) => {
  emit('delete-order', orderid);
}
const getOrderStatus = (state) => {
  return state === 0 ? '未付款' : '已完成';
};
watch(thePage, () => {
  loadPageOrder();
});
const getimageURL = (image) => {
  URL = `../assets/images/${image}`
  return URL
}
const loadOrderdetail = async (orderId) => {
  const res = await fetch(`https://localhost:7289/api/OrderDetails/${orderId}`);
  const datas = await res.json();
  product.value = datas.orderdetailproducts
  ticket.value = datas.orderdetailticket
  combinedData.value = [];
  ticket.value.forEach(item => {
    const matchingItem = product.value.find(el => el.ticketId === item.ticketId)
    if (matchingItem) {
      combinedData.value.push({ ...item, ...matchingItem })
    }
  })
  console.log(combinedData.value);
};
const filteredOrder = computed(() => {
  const totalPage = computed(() => Math.ceil(filteredOrder.value.length / pageSize.value));
  if (displayOrder.value === 'all') {
    return props.order;
  } else if (displayOrder.value === 'unpaid') {
    return props.order.filter(item => item.state === 0);
  } else if (displayOrder.value === 'history') {
    return props.order.filter(item => item.state !== 0);
  }
})
const loadPageOrder = () => {
  const start = (thePage.value - 1) * pageSize.value;
  const end = start + pageSize.value;
  pageorder.value = filteredOrder.value.slice(start, end);
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
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // 月份是0-based，所以要加1
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};
</script>
  
<style scoped>
.cute-table {
  width: 100%;
  border-collapse: collapse;
  background-color: #f5f5f5;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
}

.cute-table th,
.cute-table td {
  padding: 12px;
  text-align: center;
  border-bottom: 1px solid #e0e0e0;
}

.cute-table th {
  background-color: #ffcad4;
  color: #333;
}

.cute-table tbody tr:hover {
  background-color: #95e4fa;
}

.cute-table .expand-icon-container {
  width: 24px;
  height: 24px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}

.cute-table .expand-icon {
  width: 0;
  height: 0;
  border-style: solid;
  border-width: 8px 5px 0 5px;
  border-color: #555 transparent transparent transparent;
  transition: transform 0.3s;
}

.cute-table .expand-icon.rotate {
  transform: rotate(180deg);
}

/* 過渡效果 */
.fade-enter-active,
.fade-leave-active {
  transition: all 0.3s;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
  transform: translateY(-20px);
}

.cute-button-group {
  display: flex;
  justify-content: center;
  margin-bottom: 20px;
}

.cute-button-group button {
  padding: 8px 16px;
  margin: 0 4px;
  font-size: 16px;
  border: none;
  border-radius: 20px;
  cursor: pointer;
}

.btn-primary {
  background-color: #ffcc80;
  color: #333;
}

.btn-secondary {
  background-color: #f5f5f5;
  color: #777;
}

.btn-primary:hover,
.btn-secondary:hover {
  background-color: #ffe0b2;
}

i {
  font-size: 30px;
}

i:hover {
  color: red;
  cursor: pointer;
}
img{
  height: 50px;
  width: 50px;
}
</style>
   