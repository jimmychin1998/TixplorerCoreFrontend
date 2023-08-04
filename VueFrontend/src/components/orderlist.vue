<template>
  <div class="profile-container">

    <ul class="nav-list">

      <h4 class="text-center">Hi~{{members.name}}</h4>
      <br>
      <h6 class="text-center">您目前的點數：</h6>
      <br>
      <h6 class="text-center"><strong>{{ members.bonusPoint}}</strong>點</h6>
      <br>
      
      <a :href="EditMemberAddress"><li><i class="bi bi-person-circle"></i>會員資料</li></a>
      <!-- <li><i class="bi bi-people-fill"></i>會員修改</li> -->
      <hr>
      <router-link to="/order"><li><i class="bi bi-clipboard-fill"></i>訂單管理</li></router-link>
      <a href="https://localhost:7289/Members/myFavorite">
        <li><i class="bi bi-person-heart" style="color: pink"></i>我的最愛</li>
        </a>
      <li><i class="bi bi-box-arrow-left"></i>管理登入</li>
      <li><i class="bi bi-credit-card-2-back-fill" style="color: blue;"></i>付款資料</li>
      <li><i class="bi bi-shop-window" style="color: yellowgreen;"></i>積分商店</li>
      <router-link to="/CouponList"><li><i class="bi bi-ticket-detailed" style="color: green;"></i>我的優惠券
      </li></router-link>
      <li><i class="bi bi-database-check"></i>積分管理</li>
      <li><i class="bi bi-envelope-fill"></i>發問問題</li>
      <hr>
      <router-link to="/problem"><li><i class="bi bi-chat-square-text-fill"></i>常見問題</li></router-link>
      <li><i class="bi bi-box-arrow-right"></i>登出</li>
    </ul>
  </div>
</template>
    
<script setup>
  import {ref} from 'vue'
  import GlobalVar from "../stores/GlobalVar.js";

  const Mid = ref('M20010001')
  const members = ref('')
  const EditMemberAddress = ref(`${GlobalVar.MVClocalAddress}/Members/Edit`)
  const loadMembers = async () => {
        const res = await fetch(`https://localhost:7289/api/MembersAPI/${Mid.value}`)
        const datas = await res.json()
        members.value = datas
        console.log(members.value)
}
loadMembers()
</script>
    
<style scoped>
.profile-container {
  display: flex;
  align-items: center;
  background-color: #f0f5ff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}


.nav-list {
  list-style: none;
  margin: 0;
  padding: 0;
  margin-left: 20px;
}

.nav-list li {
  padding: 10px 16px;
  border-radius: 20px;
  background-color: #f0f5ff;
  color: #555;
  cursor: pointer;
  transition: background-color 0.2s ease, color 0.2s ease;
  font-weight: bold;
  line-height: 1.5;
  border: 1px solid black;
  margin-bottom: 10px;
  /* 設定行高，避免文字過於疊加 */
}

.nav-list li:hover {
  background-color: #86c3ff;
  color: white;
}

.router-link-active {
  color: red;
}
</style>