<template>
  <div class="accordion" id="accordionExample">
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingOne">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne"
          aria-expanded="true" aria-controls="collapseOne">
          訂單問題
        </button>
      </h2>
      <div id="collapseOne" class="accordion-collapse collapse show" aria-labelledby="headingOne">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemorder', 'order1')">訂單狀態</a><br>
          <a href="javascript:void(0);" @click="changePage('problemorder', 'order2')">付款問題</a><br>
          <a href="javascript:void(0);" @click="changePage('problemorder', 'order3')">退換貨</a><br>
          <a href="javascript:void(0);" @click="changePage('problemorder', 'order4')">取消訂單</a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingTwo">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseTwo"
          aria-expanded="false" aria-controls="collapseTwo">
          積分累積
        </button>
      </h2>
      <div id="collapseTwo" class="accordion-collapse collapse show" aria-labelledby="headingTwo">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemintegral', 'integral1')">積分累積方式</a><br>
          <a href="javascript:void(0);" @click="changePage('problemintegral', 'integral2')">積分兌換與使用
          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemintegral', 'integral3')">積分計畫相關問題
          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingThree">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseThree"
          aria-expanded="false" aria-controls="collapseThree">
          註冊帳號
        </button>
      </h2>
      <div id="collapseThree" class="accordion-collapse collapse show" aria-labelledby="headingThree">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemregister', 'register1')">帳號註冊
          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemregister', 'register2')">註冊安全
          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingFour">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFour"
          aria-expanded="false" aria-controls="collapseFour">
          付款類別
        </button>
      </h2>
      <div id="collapseFour" class="accordion-collapse collapse show" aria-labelledby="headingFour">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemcard', 'card1')">付款方式
          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemcard', 'card2')">付款金額
          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingFive">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseFive"
          aria-expanded="false" aria-controls="collapseFive">
          飯店訂房
        </button>
      </h2>
      <div id="collapseFive" class="accordion-collapse collapse show" aria-labelledby="headingFive">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemrestaurant', 'restaurant1')">飯店選擇
          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemrestaurant', 'restaurant2')">預定問題
          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingSix">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSix"
          aria-expanded="false" aria-controls="collapseSix">
          會員權益
        </button>
      </h2>
      <div id="collapseSix" class="accordion-collapse collapse show" aria-labelledby="headingSix">
        <div class="accordion-body">

          <a href="javascript:void(0);" @click="changePage('problemmember', 'member1')">會員資格
          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemmember', 'member2')">會員專屬優惠
          </a><br>

        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingSeven">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseSeven"
          aria-expanded="false" aria-controls="collapseSeven">
          優惠代碼
        </button>
      </h2>
      <div id="collapseSeven" class="accordion-collapse collapse show" aria-labelledby="headingSeven">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemdiscount', 'discount1')">優惠代碼使用

          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemdiscount', 'discount2')">優惠代碼發放

          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingEight">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseEight"
          aria-expanded="false" aria-controls="collapseEight">
          行程方式
        </button>
      </h2>
      <div id="collapseEight" class="accordion-collapse collapse show" aria-labelledby="headingEight">
        <div class="accordion-body">
          <a href="javascript:void(0);" @click="changePage('problemjourney', 'journey1')">旅遊方式選擇


          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemjourney', 'journey2')">交通方式


          </a><br>
          <a href="javascript:void(0);" @click="changePage('problemjourney', 'journey3')">導遊服務


          </a><br>
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" id="headingNine">
        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseNine"
          aria-expanded="false" aria-controls="collapseNine">
          相關政策
        </button>
      </h2>
      <div id="collapseNine" class="accordion-collapse collapse show" aria-labelledby="headingNine">
        <div class="accordion-body">

          <a href="javascript:void(0);" @click="changePage('problempolicy', 'policy1')">其他政策


          </a><br>
          <a href="javascript:void(0);" @click="changePage('problempolicy', 'policy2')">隱私政策


          </a><br>
        </div>
      </div>
    </div>
  </div>
</template>
    
<script setup>
import { defineEmits } from 'vue';
import { useRoute, useRouter } from 'vue-router';
const route = useRoute();
const router = useRouter();
const emit = defineEmits();
const scrollToElement = (elementId) => {
  const element = document.getElementById(elementId);
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
};
const changePage = (pageName, collapseId) => {
  router.push({ path: `/problemquestion/${pageName}` });
  emit('change-page', { pageName, collapseId });
  scrollToElement(collapseId);
}
</script>
    
<style scoped>
.accordion-body>a {
  display: block;
  color: black;
  font-size: 16px;
  margin: 5px;
  text-decoration: none;
}

.accordion-body>a:hover {
  color: orange;
}
</style>