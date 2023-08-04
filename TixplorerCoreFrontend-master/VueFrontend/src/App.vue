<template>
  <header class="sticky-top">
    <nav class="navbar navbar-expand-lg bg-gradient navbarHome">
      <div class="container-fluid">
        <a type="button" class="navbar-brand" @click="goHomePage"
          ><img src="./assets/images/tixplorer.png"
        /></a>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav mb-2 mb-lg-0">
            <!-- <li class="nav-item d-none d-s-block">
                            <form class="d-flex" role="search">
                                <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
                                <button class="btn btn-outline-success bg-info text-light" type="submit">Search</button>
                            </form>
                        </li> -->
            <li class="nav-item dropdown">
              <a
                class="nav-link dropdown-toggle"
                href="#"
                id="navbarDropdown"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                旅遊
              </a>
              <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                <li>
                  <a class="dropdown-item" href="javascript:void(0)">高雄</a>
                </li>
                <li>
                  <a class="dropdown-item" href="javascript:void(0)">台北</a>
                </li>
                <li>
                  <a class="dropdown-item" href="javascript:void(0)">台中</a>
                </li>
                <li>
                  <a class="dropdown-item" href="javascript:void(0)">苗栗</a>
                </li>
                <li>
                  <a class="dropdown-item" href="javascript:void(0)">台南</a>
                </li>
              </ul>
            </li>
          </ul>

          <ul class="navbar-nav mb-2 mb-lg-0 autoright">
            <li class="nav-item">
              <a
                class="nav-link"
                id="Cart"
                :href="CartAddress"
                >購物車</a
              >
            </li>
            <li class="nav-item">
              <a
                class="nav-link"
                id="Register"
                :href="RegisterAddress"
                v-if="isRegisterVisible == true"
                >{{ RegisterTitle }}</a
              >
            </li>
            <li class="nav-item">
              <a
                class="nav-link"
                id="Login"
                aria-current="page"
                :href="LoginAddress"
                >{{ LoginTitle }}</a
              >
            </li>
            <li class="nav-item">
              <a class="nav-link" :href="FAQAddress">常見問題</a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="javascript:void(0)">聯絡我們</a>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>
  <RouterView></RouterView>
  <footer class="sticky-bottom">
    <div class="container-fluid">
            <div class="row justify-content-center text-center text-bg-dark">
                <div class="col-5 align-content-center text-start">
                    <div class="item d-flex justify-content-center align-items-center">
                        <div class="mt-3 mb-2">
                            <img src="./assets/images/TitleLogo.png" />
                        </div>
                        <div>
                            <div class="mb-2">
                                網站僅供練習使用，若有侵權疑慮
                            </div>
                            <div class="mb-2">
                                請聯絡信箱：WeiTest1024@gmail.com
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-5 align-content-center text-start">
                    <div class="item">
                        <div class="mt-3 mb-3">
                        <h5>服務條款</h5>
                        </div>
                        <div class="mb-3">
                            <div href="javascript:void(0)" class="mb-3" id="">服務條款</div>
                        </div>
                        <div class="mb-3">
                            <div href="javascript:void(0)" class="mb-3" id="">隱私權政策</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  </footer>
</template>

<script setup>
import { ref } from "vue";
import { useRoute } from "vue-router";
const route = useRoute();
import { RouterLink, RouterView } from "vue-router";
import router from "./router/index.js";
import GlobalVar from "./stores/GlobalVar.js";
import LoginView from "./views/LoginView.vue";

const LoginAddress = ref("./Login");
const LoginTitle = ref("");
const RegisterAddress = ref("./RegisterValid");
const RegisterTitle = ref("");
const CartAddress = ref(`${GlobalVar.MVClocalAddress}/Cart/CartView`);
const FAQAddress = ref("/problem");

const isRegisterVisible = ref(false);

const isLogin = () => {
  if (
    sessionStorage.getItem("TIXPLORER_USER_DATA") != null &&
    sessionStorage.getItem("TIXPLORER_USER_DATA") != ""
  ) {
    isRegisterVisible.value = true;
    LoginTitle.value = "會員登出";
    RegisterTitle.value = "會員中心";
    LoginAddress.value = `${GlobalVar.MVClocalAddress}/Home/Logout`;
    RegisterAddress.value = `${GlobalVar.MVClocalAddress}/Members/Edit`;
  } else {
    isRegisterVisible.value = true;
    LoginTitle.value = "會員登入";
    RegisterTitle.value = "會員註冊";
  }
};

const goHomePage = () => {
  location.href = `${GlobalVar.MVClocalAddress}`;
};

mounted: {
  isLogin();
}
</script>
