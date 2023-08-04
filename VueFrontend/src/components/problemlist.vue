<template>
    <div class="container">
        <h1 class="text-center">請問需要什麼幫助嗎?</h1>
        <div class="row">
            <div class="col-3 d-flex justify-content-around">
                <router-link to="/order" class="link-item">
                    <img src="../assets/images/orderimage.png" alt="Order Icon">
                    <span class="link-text">查看訂單</span>
                </router-link>
            </div>
            <div class="col-3 d-flex justify-content-around">
                <a href="#" class="link-item">
                    <img src="../assets/images/discount.png" alt="Order Icon">
                    <span class="link-text">優惠活動</span>
                </a>
            </div>
            <div class="col-3 d-flex justify-content-around">
                <a href="https://localhost:7289/Members/Edit" class="link-item">
                    <img src="../assets/images/member.png" alt="Order Icon">
                    <span class="link-text">會員管理</span>
                </a>
            </div>
            <div class="col-3 d-flex justify-content-around">
                <a href="#" class="link-item">
                    <img src="../assets/images/購物車.png" alt="Order Icon">
                    <span class="link-text">查看購物車</span>
                </a>
            </div>
        </div>
    </div>
</template>
    
<script setup>

</script>
    
<style scoped>
.container {
    padding: 20px;
}

.text-center {
    text-align: center;
}

.row {
    display: flex;
    flex-wrap: wrap;
}

.col-3 {
    width: 25%;
    padding: 10px;
}

.link-item {
    display: block;
    text-align: center;
    text-decoration: none;
    color: #333;
    transition: transform 0.3s ease;
}

.link-item:hover {
    transform: translateY(-5px);
}

.icon {
    width: 100px;
    height: 100px;
    object-fit: cover;
    border-radius: 50%;
    margin-bottom: 10px;
}

.link-text {
    font-size: 16px;
}
</style>