<template>
  <div class="row justify-content-center">
    <div class="d-none d-lg-block col-lg-7">
      <div
        id="carouselExampleAutoplaying"
        class="carousel slide mb-5 tidy shadow"
        data-bs-ride="carousel"
      >
        <div class="carousel-inner">
          <div class="carousel-item active">
            <img src="@/assets/images/101大樓.jpg" class="w-100" alt="..." />
          </div>
          <div class="carousel-item">
            <img src="@/assets/images/台灣風景1.jpg" class="w-100" alt="..." />
          </div>
          <div class="carousel-item">
            <img src="@/assets/images/台灣風景2.jpg" class="w-100" alt="..." />
          </div>
        </div>
      </div>
    </div>
    <div class="col-auto col-lg-3 text-center">
      <form class="border border-info-subtle border-3 shadow">
        <h1>會員登入</h1>
        <div class="mb-3">
          <input
            type="text"
            id="Account"
            name="Account"
            class="Account"
            v-model="account"
            placeholder="帳號 Email或手機號碼"
          />
        </div>
        <div class="mb-3">
          <input
            type="Password"
            id="Password"
            name="Password"
            class="Password"
            v-model="password"
            placeholder="密碼 Password"
          />
        </div>
        <div class="mb-3">
          <p id="ImageCode"></p>
          <input
            id="validText"
            class="validText mb-3"
            type="text"
            v-model="imgcode"
            placeholder="請輸入驗證碼"
            autocomplete="off"
          />
          <div class="validCodeArea mb-3">
            <canvas id="auth-code" class="auth-code"></canvas>
            <i
              type="button"
              id="changeImage"
              class="changeImage bi bi-arrow-clockwise btn btn-info"
            ></i>
          </div>
          <span id="errorText" class="errorText">{{ errorText }}</span>
        </div>
        <input
          type="button"
          class="loginBtn mb-3 btn btn-info"
          @click="toLogin()"
          value="登入"
        />
        <div class="mx-auto mb-2">
          <router-link to="ForgetPwd">忘記密碼</router-link>
        </div>
        <div class="d-flex justify-content-center mb-2">
          <div>還沒有會員嗎？ <router-link to="RegisterValid">註冊會員</router-link></div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import codetxt from "../stores/validCodeImage.js";
import GlobalVar from "../stores/GlobalVar.js";
import { ref } from "vue";
import { useRoute } from "vue-router";
const route = useRoute();
import router from "../router/index.js";

const account = ref("");
const password = ref("");
const imgcode = ref("");
const errorText = ref("");

//點選「登入」，呼叫Login使用api
const toLogin = async () => {
  // console.log(codetxt.txtvalue);
  //驗證資料完整性
  if (validData() == "驗證OK") {
    const reponse = await fetch(
      `${GlobalVar.localWebApiAddress}/api/Members/Login?account=${account.value}&password=${password.value}`
    );
    const datas = await reponse.text();
    // console.log(datas);
    switch (datas) {
      case "LoginSuccess":
        const logindatas = {
          Account: account.value,
        };
        //將帳號存入SPA專案的local session中
        sessionStorage.setItem(
          "TIXPLORER_USER_DATA",
          JSON.stringify(logindatas)
        );

        // await fetch(
        //   `${
        //     GlobalVar.MVClocalAddress
        //   }/Home/setSession?logindatas=${JSON.stringify(logindatas)}`
        // );
        //連結至SPA專案首頁，推GITHUB需要註解掉，改用MVC首頁
        // router.push({ name: 'home'})
        if(route.query.CartDatas == null){
          location.href = `${GlobalVar.MVClocalAddress}?logindatas=${JSON.stringify(logindatas)}`;
        }else{
          location.href = `${GlobalVar.MVClocalAddress}?logindatas=${JSON.stringify(logindatas)}&CartDatas=${route.query.CartDatas}`
        }
        break;
      case "LoginFail":
        errorText.value = "帳號或密碼不符，請重新輸入";
        break;
      default:
        errorText.value = "資料異常，請稍後再試";
        break;
    }
  } else {
    errorText.value = validData();
  }
};

const validData = () => {
  //驗證帳號不可為空白
  if (account.value == null || account.value == "") return "帳號不可為空白";
  //驗證密碼不可為空白
  if (password.value == null || password.value == "") return "密碼不可為空白";
  //圖形驗證碼不可為空白
  if (imgcode.value == null || imgcode.value == "") return "請輸入驗證碼";
  //圖形驗證碼必須正確
  if (imgcode.value != codetxt.txtvalue) return "驗證碼錯誤，請重新輸入";

  //驗證通過，回傳驗證通過訊息
  return "驗證OK";
}

mounted:{
    // () => {
      if (route.query.isLoginout == 1 && sessionStorage.getItem("TIXPLORER_USER_DATA") != null && sessionStorage.getItem("TIXPLORER_USER_DATA") != ''){
      sessionStorage.removeItem("TIXPLORER_USER_DATA")
    }
  // }
};
</script>

<style scoped>
form{
    position: relative;
    width: 240px;
    margin-top: 5%;
    left:50%;
    transform: translate(-50%, 0%);
}
.row{
margin-top: 2%;
margin-bottom: 2%;
}
#canvas {
    border: 1px solid #ccc;
}
.Account{
    width: 85%;
}
.Password{
    width: 85%;
}
.validText{
    width: 85%;
}
.auth-code{
    margin-left: 10%;
    float:left;
}
.changeImage{
    margin-left: 5%;
    /* position: absolute; */
    margin-top: 5px;
    float:left;
}
.validCodeArea{
    height: 46px;
}
.loginBtn{
    width: 85%;
}
.errorText{
    color:red
  }
  </style>

