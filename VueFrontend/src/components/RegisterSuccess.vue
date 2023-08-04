<template>
  <div class="row justify-content-center">
    <div class="tableArea border border-light-subtle shadow">
      <div class="title">註冊完成</div>
      <div class="mt-1 mb-3 divline"></div>
      <div class="text-center">
        <div class="mb-3">註冊成功，以下為您的登入帳號資料，</div>
        <div class="mb-3">
          您可透過使用「電子信箱」或是「行動電話」進行登入。
        </div>
        <div class="mb-3 infobox">
          <div class="mb-3 mt-3 usertext">電子信箱：{{ email }}</div>
          <div class="mb-3 usertext">手機號碼：{{ phone }}</div>
        </div>
        <div>
          您現在可以點擊「登入」按鈕進行登入。
        </div>
      </div>
      <div class="sendValidEmailArea d-flex justify-content-center">
        <a type="button" class="btn btn-info m-3" :href="`${GlobalVar.MVClocalAddress}`">返回首頁</a>
        <a type="button" class="btn btn-warning m-3" href="/login">前往登入</a>
      </div>
    </div>
  </div>
</template>

<script setup>
import GlobalVar from "../stores/GlobalVar.js";
import { ref } from "vue";
import { useRoute } from "vue-router";
const route = useRoute();

const email = ref(JSON.parse(route.query.Registerdatas).Email)
const phone = ref(JSON.parse(route.query.Registerdatas).Phone)

</script>

<style scoped>
.row {
  margin-top: 2%;
  margin-bottom: 2%;
}
.title {
  font-weight: bold;
  font-size: 32px;
  background: linear-gradient(to top, #5a995a, #23632b);
  background-clip: text;
  -webkit-background-clip: text;
  color: transparent;
}
.tableArea {
  width: 50%;
  /* height: 450px; */
}
.row {
  margin-top: 2%;
  margin-bottom: 2%;
  width: 100%;
}
.divline{
  position: relative;
  border: 2px solid rgb(7, 190, 84, 0.25);
  width: 100%;
}
.infobox {
  position: relative;
  border-top: 2px solid rgb(7, 190, 84, 0.25);
  border-bottom: 2px solid rgb(7, 190, 84, 0.25);
  width: 70%;
  left: 15%;
}
.usertext{
  font-weight: bold;
  color: blue
}
</style>
