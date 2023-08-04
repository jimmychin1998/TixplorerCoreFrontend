<template>
  <div class="row justify-content-center">
    <div class="tableArea border border-light-subtle shadow">
      <div class="title">填寫個人資料</div>
      <div class="mt-1 mb-3 divline"></div>
      <table>
        <tbody>
          <tr>
            <th>
              <label class="mx-3">姓名</label>
              <br />
              <div class="mx-3 subtext">Your Name</div>
            </th>
            <td>
              <input
                class="mx-3 mt-3 mb-2 inputtext"
                type="text"
                v-model="name"
              />
              <br />
              <div id="errorTextName" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">會員密碼</label>
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
          <tr>
            <th>
              <label class="mx-3">行動電話</label>
              <br />
              <div class="mx-3 subtext">Mobile Phone</div>
            </th>
            <td>
              <input
                class="mx-3 mt-3 inputtext"
                type="text"
                v-model="phone"
                autocomplete="off"
              />
              <br />
              <div class="tips mx-3">請輸入純由數字組合。</div>
              <div class="tips mx-3">
                請輸入正確的行動電話號碼，避免相關權益受損
              </div>
              <div id="errorTextPhone" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">性別</label>
              <br />
              <div class="mx-3 subtext">Gender</div>
            </th>
            <td>
              <input
                class="m-2 mb-3 mt-3"
                type="radio"
                name="gender"
                value="0"
                v-model="gender"
              />男
              <span class="tips"> Male</span>
              <input
                class="m-2 mb-3 mt-3"
                type="radio"
                name="gender"
                value="1"
                v-model="gender"
              />女
              <span class="tips"> Female</span>
              <input
                class="m-2 mb-3 mt-3"
                type="radio"
                name="gender"
                value="2"
                v-model="gender"
              />不提供
              <span class="tips"> None</span>
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">居住地址</label>
              <br />
              <div class="mx-3 subtext">Address</div>
            </th>
            <td>
              <input
                class="mx-3 mt-3 inputtext"
                type="text"
                v-model="address"
              />
              <br />
              <div class="tips mx-3 mb-2">
                請輸入正確的居住地址，避免相關權益受損
              </div>
              <div id="errorTextAddress" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
          <tr>
            <th>
              <label class="mx-3">生日</label>
              <br />
              <div class="mx-3 subtext">Birthday</div>
            </th>
            <td>
              <select
                id="birthdayYear"
                name="birthdayYear"
                class="mt-3 mb-2 birthday"
                v-model="birthdayYear"
                @change="setDate"
              >
                <option v-for="year in years">{{ year }}</option>
              </select>
              <span> 年 Year</span>
              <select
                id="birthdayMonth"
                name="birthdayMonth"
                class="mt-3 mb-2 birthday"
                v-model="birthdayMonth"
                @change="setDate"
              >
                <option v-for="month in 12">{{ month }}</option>
              </select>
              <span> 月 Month</span>
              <select
                id="birthdayDate"
                name="birthdayDate"
                class="mt-3 mb-2 birthday"
                v-model="birthdayDate"
              >
                <option v-for="date in dates">{{ date }}</option>
              </select>
              <span> 日 date</span>
              <div id="errorTextBirthday" class="errorText mx-3 mb-2"></div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="sendValidEmailArea d-flex justify-content-center">
        <input
          type="button"
          @click="registerMember"
          class="btn btn-warning m-3"
          value="確認送出"
        />
      </div>
      <div class="d-flex justify-content-center mb-2">
        <span id="errorText" class="errorText">{{ errorText }}</span>
      </div>
    </div>
  </div>
</template>

<script setup>
import GlobalVar from "../stores/GlobalVar.js";
import { useRoute } from "vue-router";
const route = useRoute();
import router from "../router/index.js";
import { ref } from "vue";

const name = ref("");
const password = ref("");
const pwdComfirm = ref("");
const phone = ref("");
const gender = ref(0);
const address = ref("");
const birthdayYear = ref(new Date().getFullYear() - 18); //假定使用平台的人皆已滿18歲，預設為當下年份-18
const birthdayMonth = ref(1);
const birthdayDate = ref(1);
const errorText = ref("");

//使用者送出資料並註冊
const registerMember = async () => {
  if (validData()) {
    //資料驗證成功，進行註冊作業

    let packBirthday = formatBirthday();
    //打包註冊資料
    const userDatas = {
      Email: JSON.parse(route.query.packDatas).Email,
      Phone: phone.value,
      Password: password.value,
      Name: name.value,
      Sex: gender.value,
      Birthday: packBirthday,
      Address: address.value,
    };

    //測試用驗證打包資料
    // console.log(userDatas);

    //傳送註冊資料給 API 進行處理
    const response = await fetch(
      `${
        GlobalVar.localWebApiAddress
      }/api/Members/registerMember?datas=${JSON.stringify(userDatas)}`
    );
    const datas = await response.text();

    //根據回傳資料執行報錯或註冊成功
    switch (datas) {
      case "RegisterSuccess":
        const Registerdata = {
          Email: JSON.parse(route.query.packDatas).Email,
          Phone: phone.value,
        };

        const Registerdatas = JSON.stringify(Registerdata);
        //前往註冊成功畫面
        router.push({ name: "RegisterSuccess", query: { Registerdatas } });

        break;
      case "RegisterFail":
        errorText.value = "註冊失敗";
        break;
      default:
        errorText.value = "資料異常，請稍後再試";
        break;
    }
  }
};

//設定可選的年份，可選年份為當下年份至100年前，假定使用者最高齡為100歲
const setYears = () => {
  let yearDatas = [];
  //將加入年份由大至小加入陣列中
  for (
    let i = new Date().getFullYear();
    i >= new Date().getFullYear() - 100;
    i--
  ) {
    yearDatas.push(i);
  }
  //回傳陣列至可選年份的變數years中
  return yearDatas;
};

//根據年月設定可選擇日期
const setDate = () => {
  //因為透過下拉選單選擇值後會變成字串，故用Number進行轉型
  switch (Number(birthdayMonth.value)) {
    case 1:
    case 3:
    case 5:
    case 7:
    case 8:
    case 10:
    case 12:
      dates.value = 31;
      break;
    case 4:
    case 6:
    case 9:
    case 11:
      dates.value = 30;
      break;
    default:
      //閏年規則：
      //1.可被4整除但不能被100整除
      //2.可被100整除且同時可被400整除
      if (
        (Number(birthdayYear.value) % 4 == 0 &&
          Number(birthdayYear.value) % 100 != 0) ||
        Number(birthdayYear.value) % 400 == 0
      )
        dates.value = 29;
      else dates.value = 28;
      break;
  }
};

const years = ref(setYears());
let dates = ref(31);

const formatBirthday = () => {
  let packBirthdayYear = birthdayYear.value;
  let packBirthdayMonth = birthdayMonth.value;
  let packBirthdayDate = birthdayDate.value;

  if (packBirthdayMonth.toString().length <= 1) {
    packBirthdayMonth = `0${packBirthdayMonth}`;
    console.log(packBirthdayMonth);
  }
  if (packBirthdayDate.toString().length <= 1) {
    packBirthdayDate = `0${packBirthdayDate}`;
    console.log(packBirthdayDate);
  }

  const packBirthday =
    packBirthdayYear + "-" + packBirthdayMonth + "-" + packBirthdayDate;

  return packBirthday;
};

//驗證填寫資料
const validData = () => {
  //設定變數控制是否驗證通過，任一項目沒驗證通過時改變其值為1
  let result = 0;

  //-------------------------------------------------------------------
  //姓名不可為空白
  if (name.value == null || name.value == "") {
    document.getElementById("errorTextName").innerText = "姓名不可為空白";
    result = 1;
  } else {
    document.getElementById("errorTextName").innerText = "";
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
  if (phone.value == null || phone.value == "") {
    document.getElementById("errorTextPhone").innerText = "行動電話不可為空白";
    result = 1;
  } else {
    //行動電話不為空白時驗證
    //行動電話必須由純數字組成
    let regex = /^[0-9]+$/;
    if (!regex.test(phone.value)) {
      document.getElementById("errorTextPhone").innerText =
        "行動電話只能由純數字組成";
      result = 1;
    } else {
      document.getElementById("errorTextPhone").innerText = "";
    }
  }
  //-------------------------------------------------------------------
  //居住地址不可為空白
  if (address.value == null || address.value == "") {
    document.getElementById("errorTextAddress").innerText =
      "居住地址不可為空白";
    result = 1;
  } else {
    document.getElementById("errorTextAddress").innerText = "";
  }
  //-------------------------------------------------------------------
  //生日必須選擇
  if (
    birthdayYear.value == 0 ||
    birthdayMonth.value == 0 ||
    birthdayDate.value == 0
  ) {
    document.getElementById("errorTextBirthday").innerText = "請選擇生日年月日";
    result = 1;
  } else {
    document.getElementById("errorTextBirthday").innerText = "";
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
  margin-top: 2%;
  margin-bottom: 2%;
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
.birthday {
  margin-left: 15px;
  width: 80px;
}
.divline {
  position: relative;
  border: 2px solid rgb(7, 190, 84, 0.25);
  width: 100%;
}
</style>
