<template>
  <div class="row justify-content-center">
    <div class="tableArea border border-light-subtle shadow">
      <div class="title">驗證電子信箱</div>
      <div class="mt-1 mb-3 divline"></div>
      <table>
        <tbody>
          <tr>
            <th>
              <label class="mx-3">電子信箱</label>
              <br />
              <div class="mx-3 subtext">Email</div>
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
              <div class="tips mx-3 mb-2">該電子信箱將會註冊為帳號使用</div>
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
              <br>
              <div class="mx-3 subtext">Valid Code</div>
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
        </tbody>
      </table>
      <div class="sendValidEmailArea d-flex justify-content-center mb-1">
        <input
          type="button"
          @click="nextStep()"
          class="btn btn-info mt-3 mb-2"
          value="下一步"
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
const imgcode = ref("");
const mailText = ref("");
const errorText = ref("");
const emailCode = ref("");

//點選「送出驗證信件」，呼叫 sendValidEmail 呼叫 API 發送驗證碼郵件
const sendValidEmail = async () => {
  if (validEmailData() == "驗證OK") {
    //驗證是否有填入信箱
    //產生信箱驗證碼
    validMail.doGenerateValidMailCode();

    //快速取得驗證碼使用↓
    // console.log(validMail.validMailCode.txtvalue)

    const response = await fetch(
      `${GlobalVar.localWebApiAddress}/api/Members/sendValidEmail?validCode=${validMail.validMailCode.txtvalue}&email=${email.value}`
    );
    const datas = await response.text();
    console.log(validMail.validMailCode.txtvalue)
    console.log(email.value)
    console.log(validEmailData())
    console.log(datas)
    


    switch (datas) {
      case "SendSuccess":
        document.getElementById("mailText").style.color = "blue";
        mailText.value = "驗證信件已發送";
        break;
      case "isRegister":
        document.getElementById("mailText").style.color = "red";
        mailText.value = "該信箱已註冊，請更換信箱";
        break;
      default:
        document.getElementById("mailText").style.color = "red";
        mailText.value = "發送驗證信件失敗，請稍後再試";
        break;
    }
  } else {
    document.getElementById("mailText").style.color = "red";
    mailText.value = validEmailData();
  }
};

//點選「下一步」，呼叫 nextStep 跳轉至下個頁面
const nextStep = async () => {
  //   console.log(codetxt.txtvalue);
  //驗證資料完整性
  if (validNextData() == "驗證OK") {
    //打包使用者輸入的電子信箱
    const packData = {
      Email: email.value,
    };
    //轉換成JSON格式傳送
    const packDatas = JSON.stringify(packData);

    //跳轉到註冊畫面 - 填寫個人資料
    router.push({ name: "Register", query: { packDatas } });
    // location.href = `/Register?packDatas=${packDatas}`;
  } else {
    errorText.value = validNextData();
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

const validNextData = () => {
  //信箱不可為空白
  if (email.value == null || email.value == "") return "電子信箱不可為空白";
  //圖形驗證碼不可為空白
  if (imgcode.value == null || imgcode.value == "") return "請輸入驗證碼";
  //圖形驗證碼必須正確
  if (imgcode.value != codetxt.txtvalue) return "驗證碼錯誤，請重新輸入";
  //信箱驗證碼必須正確
  if (emailCode.value != validMail.validMailCode.txtvalue)
    return "信箱驗證碼錯誤，請重新輸入";

  //驗證通過，回傳驗證通過訊息
  return "驗證OK";
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
.inputtext {
  width: 60%;
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
.divline{
  position: relative;
  border: 2px solid rgb(7, 190, 84, 0.25);
  width: 100%;
}
</style>
