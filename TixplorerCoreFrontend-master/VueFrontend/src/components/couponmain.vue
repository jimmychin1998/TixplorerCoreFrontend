<template>
    <div class="cute-button-group mt-3">
        <a href="#" class="btn btn-primary" @click="clickbutton('false')">未使用</a>
        <a href="#" class="btn btn-primary" @click="clickbutton('true')">使用紀錄</a>
    </div>
    <div v-if="history">
        <div class="row">
            <div class="col-4"></div>
            <div class="col-4">
                <img src="../assets/images/青蛙哭哭.gif">
                <a href="https://localhost:7289/Search?productType=1&productCondition=0" class="astyle">
                    <img src="../assets/images/run.png">
                    <span>快去使用</span>
                </a>
            </div>
            <div class="col-4"></div>
        </div>

    </div>
    <div v-else>
        <table class="cute-table">
            <thead>
                <tr>
                    <th>優惠券名稱</th>
                    <th>折扣額度</th>
                    <th>使用期限</th>
                    <th>使用範圍</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="{ name, discount, expirationDate } in coupon">
                    <td>{{ name }}</td>
                    <td>{{ discount }}</td>
                    <td>{{ formatDate(expirationDate) }}</td>
                    <td>全館商品</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
    
<script setup>
import { defineProps, ref } from 'vue'
const props = defineProps({
    couponlist: Array,
    coupon: Array
});
console.log(props.coupon.value)
const history = ref(false)
const clickbutton = (state) => {
    if (state == 'true') {
        history.value = true
    }
    else {
        history.value = false
    }

}
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // 月份是0-based，所以要加1
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};
</script>
    
<style scoped>

.astyle {
    display: block;
    text-align: center;
    text-decoration: none;
    color: #333;
    transition: transform 0.3s ease;
    border: 1px solid black;
    border-radius: 20px;
    margin: 50px;
}

.astyle:hover {
    cursor: pointer;
    background-color: gold;
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
</style>