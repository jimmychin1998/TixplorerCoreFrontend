<template>
  <div class="row justify-content-center">
    <div class="tableArea border border-light-subtle shadow">
      <div class="title">密碼重設成功</div>
      <div class="mt-1 mb-3 divline"></div>
      <div class="text-center">
        <div class="mb-3">您已成功重設密碼，</div>
        <div class="mb-3">
          您可透過使用「電子信箱」或是「行動電話」進行登入。
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

</script>

<style scoped>
.row {
  margin-top: 2%;
  margin-bottom: 2%;
  width: 100%;
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
  width: 100%;
}
.divline{
  position: relative;
  border: 2px solid rgb(7, 190, 84, 0.25);
  width: 100%;
}
.usertext{
  font-weight: bold;
  color: blue
}
</style>
