<template>
    <h2 class="text-center my-5">問題分類</h2>
    <div class="row">
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemorder" class="link-item">
                <img src="../assets/images/01.png">
                <p class="fs-5 fw-bolder">訂單問題</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemintegral" class="link-item">
                <img src="../assets/images/02.png">
                <p class="fs-5 fw-bolder">積分累積</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemregister" class="link-item">
                <img src="../assets/images/03.png">
                <p class="fs-5 fw-bolder">註冊帳號</p>

            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemcard" class="link-item">
                <img src="../assets/images/04.png">
                <p class="fs-5 fw-bolder">付款類別</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemrestaurant" class="link-item">
                <img src="../assets/images/05.png">
                <p class="fs-5 fw-bolder">飯店訂房</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemmember" class="link-item">
                <img src="../assets/images/06.png">
                <p class="fs-5 fw-bolder">會員權益</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemdiscount" class="link-item">
                <img src="../assets/images/07.png">
                <p class="fs-5 fw-bolder">優惠代碼</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problemjourney" class="link-item">
                <img src="../assets/images/08.png">
                <p class="fs-5 fw-bolder">行程方式</p>
            </router-link>
        </div>
        <div class="col-6 col-sm-3">
            <router-link to="/problemquestion/problempolicy" class="link-item">
                <img src="../assets/images/09.png">
                <p class="fs-5 fw-bolder">相關政策</p>       
            </router-link>
        </div>
    </div>
</template>
    
<script setup>
</script>
    
<style scoped>
.link-item {
    display: block;
    width: 100%;
    height: 100%;
    text-align: center;
    text-decoration: none;
    padding: 10px;
    border-radius: 5px;
    transition: transform 0.5s ease;
}

.link-item:hover {
    transform: translateY(-5px);
    border: 1px solid #ccc;
    opacity: 0.8;
}
</style>