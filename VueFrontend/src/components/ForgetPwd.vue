<template>
  <div class="row justify-content-center">
    <div class="tableArea border border-light-subtle shadow">
      <div class="title">重設密碼</div>
      <div class="mt-1 mb-3 divline"></div>
      <table>
        <tbody>
          <tr>
            <th>
              <label class="mx-3">電子信箱</label>
              <br />
            </th>
            <td>
              <input
                class="mt-3 mx-3 inputtext"
                type="text"
                v-model="email"
                placeholder="請輸入電子信箱"
                autocomplete="off"
              />
              <br />
              <div class="tips mx-3 mb-2">輸入已註冊的電子信箱</div>
            </td>
          </tr>
          <tr>
            <th>
              <div class="validCodeArea mx-3">
                <i
                  type="button"
                  id="changeImage"
                  class="changeImage bi bi-arrow-clockwise btn btn-info"
                  style="float: right"
                ></i>
                <canvas
                  id="auth-code"
                  class="auth-code"
                  style="float: right"
                ></canvas>
              </div>
            </th>
            <td>
              <div class="mx-3">
                <p id="ImageCode"></p>
                <input
                  id="validText"
                  class="mb-3 inputtext"
                  type="text"
                  v-model="imgcode"
                  placeholder="請輸入圖型驗證碼"
                  autocomplete="off"
                />
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="sendValidEmailArea d-flex justify-content-center mb-1">
        <input
          type="button"
          @click="sendValidEmail()"
          class="btn btn-warning mt-3 mb-2"
          value="送出驗證信件"
        />
      </div>
      <div class="d-flex justify-content-center mb-2">
        <span id="mailText">{{ mailText }}</span>
      </div>
      <table>
        <tbody>
          <tr>
            <th>
              <label class="mx-3">信箱驗證碼</label>
            </th>
            <td>
              <input
                class="m-3 inputtext"
                type="text"
                v-model="emailCode"
                placeholder="請輸入信箱驗證碼"
                autocomplete="off"
              />
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">新會員密碼</label>
              <br />
              <div class="mx-3 subtext">Password</div>
            </th>
            <td>
              <input
                class="mx-3 mt-3 inputtext"
                type="password"
                v-model="password"
              />
              <br />
              <div class="tips mx-3">
                請以半形輸入由8~16個英、數字與底線組合。
              </div>
              <div class="tips mx-3 mb-2">至少須包含一個英文與數字</div>
              <div id="errorTextPassword" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">密碼確認</label>
              <br />
              <div class="mx-3 subtext">Comfirm Password</div>
            </th>
            <td>
              <input
                class="mx-3 mt-3 inputtext"
                type="password"
                v-model="pwdComfirm"
              />
              <br />
              <div class="tips mx-3 mb-2">請輸入與「會員密碼」相同</div>
              <div id="errorTextPwdComfirm" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="sendValidEmailArea d-flex justify-content-center mb-1">
        <input
          type="button"
          @click="ResetPwd()"
          class="btn btn-info mt-3 mb-2"
          value="重設密碼"
        />
      </div>
      <div class="d-flex justify-content-center mb-2">
        <span id="errorText" class="errorText">{{ errorText }}</span>
      </div>
    </div>
  </div>
</template>

<script setup>
import codetxt from "../stores/validCodeImage.js";
import validMail from "../stores/validMailCode.js";
import GlobalVar from "../stores/GlobalVar.js";
import { useRoute } from "vue-router";
const route = useRoute();
import router from "../router/index.js";
import { ref } from "vue";

const email = ref("");
const mailText = ref("");
const imgcode = ref("");
const password = ref("");
const pwdComfirm = ref("");
const errorText = ref("");
const emailCode = ref("");

//點選「送出驗證信件」，呼叫 sendValidEmail 呼叫 API 發送驗證碼郵件
const sendValidEmail = async () => {
  if (validEmailData() == "驗證OK") {
    //驗證是否有填入信箱
    //產生信箱驗證碼
    validMail.doGenerateValidMailCode();

    //快速取得驗證碼使用↓
    console.log(validMail.validMailCode.txtvalue)

    const response = await fetch(
      `${GlobalVar.localWebApiAddress}/api/Members/sendValidEmailForgetPwd?validCode=${validMail.validMailCode.txtvalue}&email=${email.value}`
    );
    const datas = await response.text();

    switch (datas) {
      case "SendSuccess":
        document.getElementById("mailText").style.color = "blue";
        mailText.value = "驗證信件已發送";
        break;
      case "isNotRegister":
        document.getElementById("mailText").style.color = "red";
        mailText.value = "該信箱未註冊，請更換正確信箱";
        break;
      default:
        document.getElementById("mailText").style.color = "red";
        mailText.value = "發送驗證信件失敗，請稍後再試";
        break;
    }
  } else {
    document.getElementById("mailText").style.color = "red";
    mailText.value = validEmailData;
  }
};

//點選「重設密碼」，呼叫 ResetPwd 方法並跳轉至下個頁面
const ResetPwd = async () => {
  //驗證資料完整性
  if (validResetData()) {
    //打包重設密碼資料
    const response = await fetch(
      `${
        GlobalVar.localWebApiAddress
      }/api/Members/resetPwd?email=${email.value}&pwd=${password.value}`
    );
    const datas = await response.text();

    switch (datas) {
      case "ResetSuccess":
        //跳轉到重設密碼成功畫面
        router.push({ name: "ForgetPwdSuccess" });
        break;
        case "ResetSuccess":
        //跳轉到重設密碼成功畫面
        router.push({ name: "ForgetPwdSuccess" });
        break;
      default:
        errorText.value = "重設密碼失敗，請稍後再試";
        break;
    }
  }
};

const validEmailData = () => {
  //信箱不可為空白
  if (email.value == null || email.value == "") return "電子信箱不可為空白";
  //圖形驗證碼不可為空白
  if (imgcode.value == null || imgcode.value == "") return "請輸入驗證碼";
  //圖形驗證碼必須正確
  if (imgcode.value != codetxt.txtvalue) return "驗證碼錯誤，請重新輸入";

  //驗證通過，回傳驗證通過訊息
  return "驗證OK";
};

//驗證填寫資料
const validResetData = () => {
  //設定變數控制是否驗證通過，任一項目沒驗證通過時改變其值為1
  let result = 0;

  //-------------------------------------------------------------------
  //信箱不可為空白
  if (email.value == null || email.value == "") {
    errorText.value = "電子信箱不可為空白";
    result = 1;
  } else {
    errorText.value = "";
  }
  //-------------------------------------------------------------------
  //圖形驗證碼不可為空白
  if (imgcode.value == null || imgcode.value == "") {
    errorText.value = "請輸入驗證碼";
    result = 1;
  } else {
    //圖形驗證碼必須正確
    if (imgcode.value != codetxt.txtvalue) {
      errorText.value = "驗證碼錯誤，請重新輸入";
      result = 1;
    } else {
      errorText.value = "";
    }
  }

  //-------------------------------------------------------------------
  //信箱驗證碼必須正確
  if (emailCode.value != validMail.validMailCode.txtvalue) {
    errorText.value = "驗證碼錯誤，請重新輸入";
    result = 1;
  } else {
    errorText.value = "";
  }
  //-------------------------------------------------------------------
  //密碼不可為空白
  if (password.value == null || password.value == "") {
    document.getElementById("errorTextPassword").innerText = "密碼不可為空白";
    result = 1;
  } else {
    //密碼不為空白時驗證
    //密碼必須由英、數字組成，並且長度介於8~16個之間，至少須包含一個英文與一個數字
    let regex1 = /^[A-Za-z0-9_]{8,16}$/;
    if (!regex1.test(password.value)) {
      document.getElementById("errorTextPassword").innerText =
        "密碼只能由英、數字與底線組合，且長度需介於8~16個之間";
      result = 1;
    } else {
      //密碼組合內容驗證成功時驗證
      //密碼必須至少包含一個英文與一個數字
      let regex2 = /^[A-Za-z]{1,}[0-9]{1,}$/;
      if (!regex2.test(password.value)) {
        document.getElementById("errorTextPassword").innerText =
          "密碼必須至少包含一個英文與一個數字";
        result = 1;
      } else {
        document.getElementById("errorTextPassword").innerText = "";
      }
    }
  }

  //-------------------------------------------------------------------
  //確認密碼不可為空白
  if (pwdComfirm.value == null || pwdComfirm.value == "") {
    document.getElementById("errorTextPwdComfirm").innerText =
      "確認密碼不可為空白";
    result = 1;
  } else {
    //確認密碼不為空白時驗證
    //確認密碼必須與會員密碼相同
    if (pwdComfirm.value != password.value) {
      document.getElementById("errorTextPwdComfirm").innerText =
        "確認密碼必須與會員密碼相同";
      result = 1;
    } else {
      document.getElementById("errorTextPwdComfirm").innerText = "";
    }
  }
  //-------------------------------------------------------------------
  //判斷是否驗證通過
  if (result == 0) return true;
  else return false;
};

</script>

<style scoped>
table {
  width: 100%;
}
tbody {
  position: relative;
  width: 400px;
}
label {
  float: right;
}
tr {
  width: 100%;
  /* height: 50px; */
  border-top: 1px dashed gray;
  border-bottom: 1px dashed gray;
}
th {
  width: 45%;
  background-color: lightgray;
}
td {
  width: 55%;
}
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
.inputtext {
  width: 60%;
}
.tableArea {
  width: 50%;
  /* height: 450px; */
}
.row {
  width: 100%;
}
.changeImage {
  margin-left: 5%;
  /* position: absolute; */
  margin-top: 5px;
  float: left;
}
.validCodeArea {
  margin: 0;
  padding: 0;
}
.subtext {
  float: right;
  color: gray;
}
.tips {
  color: gray;
}
.errorText {
  color: red;
}
.divline {
  position: relative;
  border: 2px solid rgb(7, 190, 84, 0.25);
  width: 100%;
}
</style>
