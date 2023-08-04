<template>
    <div class="margin">
        <h1  ref="myHeading" id="register1">註冊帳號</h1>
        <h3>帳號註冊</h3>
        <toggleContent title="我試圖註冊帳號，但系統顯示我的電子郵件已被使用，該怎麼辦？">
            建議客戶檢查是否已經註冊過該郵件地址，如果忘記密碼，可以使用「忘記密碼」功能來恢復帳號存取權限。
        </toggleContent>
        <toggleContent title="我嘗試註冊帳號時出現錯誤，無法完成註冊，該怎麼處理？">
            提醒客戶檢查輸入的資訊是否正確，例如郵件地址是否有效、密碼是否符合要求，或嘗試使用其他瀏覽器進行註冊。
        </toggleContent>
        <toggleContent title="我已經註冊了帳號，但無法登錄，該怎麼處理？">
            確保客戶輸入的帳號和密碼正確，並嘗試使用「忘記密碼」功能重設密碼。
        </toggleContent>
        <toggleContent title="是否可以使用社交媒體帳號註冊？如果可以，如何操作？">
            沒錯，我們支持社交媒體帳號註冊，並且包含GOOGLE登入、FB快速註冊、IG快速註冊...等皆可以在我們註冊頁面中進行註冊
        </toggleContent>
        <toggleContent title="我希望註冊帳號時保持匿名，是否可以註冊無需提供個人資料的帳號？">
            很抱歉，由於身份驗證、使用者安全、更好的服務、合法遵循...等原因，我們目前並未提供匿名服務，請填寫真實資料。
        </toggleContent>
        <br ref="myHeading" id="register2">
        <br>
        <h3>註冊安全</h3>
        <toggleContent title="我收到一封來自不明來源的電子郵件，要求我提供帳號資訊，這是否合法？">
            不要回覆或提供個人資訊給不明來源的郵件，以防止釣魚詐騙行為。建議客戶直接在官方網站上處理帳號相關事務。
        </toggleContent>
        <toggleContent title="我擔心我之前使用的密碼太弱，該如何更改密碼？">
            指導客戶如何在帳號設置中更改密碼，並建議使用較強的密碼組合。
        </toggleContent>
        <toggleContent title="我擔心我的帳號安全，有什麼方法可以增強帳號的安全性？">
            可以使用強密碼、啟用雙因素驗證、定期更換密碼....等增強您的帳號安全性
        </toggleContent>
        <toggleContent title="我希望了解該旅遊網站的帳號資料是否會被共用或出售給第三方？">
            提供關於帳號資料保護和隱私政策的相關說明，以及確保客戶的帳號資料只會用於相關的旅遊服務目的。
        </toggleContent>
        <toggleContent title="我想知道如果我的帳號遭到黑客攻擊或資料外洩，該怎麼處理？">
            解釋該旅遊網站的帳號安全措施，並提供相關聯繫方式，以便客戶報告任何安全問題或資料外洩事件。同時，建議客戶及時更新密碼並監控帳號活動，以防範未來的風險。
        </toggleContent>
    </div>
</template>

<script setup>
import toggleContent from '../components/ToggleComponent.vue'
import { ref, watch } from 'vue';
const myHeading = ref(null);
const props = defineProps({
    scrollToId: String
})
watch(() => props.scrollToId, (newId) => {
    if (newId) {

        setTimeout(() => {
            const targetElement = document.getElementById(newId);
            if (targetElement) {
                targetElement.scrollIntoView({ behavior: 'smooth' });
            }
        }, 0);
    }
});
</script>

<style scoped>
.margin>h1 {
    margin-top: 20px;
}

.margin>h3 {
    margin-top: 30px;
    margin-bottom: 20px;
    color: red;
}
</style>