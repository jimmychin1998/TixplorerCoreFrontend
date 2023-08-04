<template>
    <div class="margin">
        <h1 ref="myHeading" id="member1">會員權益</h1>
        <h3>會員資格</h3>
        <toggleContent title="我該如何申請成為會員？">
            前往網站或應用程式上的會員註冊頁面，填寫必要的個人資訊並按下註冊按鈕即可成為會員。
        </toggleContent>
        <toggleContent title="會員資格是否需要付費？">
            會員資格通常是免費的，無需支付任何費用。
        </toggleContent>
        <toggleContent title="會員資格是否有有效期限？">
            通常會員資格是無期限的，一旦成為會員，便可永久享受會員權益。
        </toggleContent>
        <toggleContent title="是否有年齡限制或其他條件成為會員？">
            年齡限制通常是18歲以上，其他條件可能包括提供有效的電子郵件地址和同意隱私政策等。
        </toggleContent>
        <toggleContent title="我可以申請家庭會員資格嗎？">
            部分旅遊平台支援家庭會員資格，讓家庭成員共享會員權益，需在註冊時選擇家庭會員選項。
        </toggleContent>
        <br ref="myHeading" id="member2">
        <br>
        <h3>會員專屬優惠</h3>
        <toggleContent title="會員專屬優惠有哪些？">
            會員專屬優惠通常包括獨家折扣、積分返還、生日禮遇、升級服務等。
        </toggleContent>
        <toggleContent title="怎麼查看我的會員專屬優惠？">
            登入會員帳號後，在個人頁面或專屬會員專區查看專屬優惠。
        </toggleContent>
        <toggleContent title="如何使用會員專屬優惠？">
            在預訂或結帳過程中，確保登入會員帳號，系統將自動套用專屬優惠。
        </toggleContent>
        <toggleContent title="會員優惠是否適用於所有預訂？">
            會員優惠可能僅適用於特定預訂類型或合作夥伴，建議查閱優惠條款細節。
        </toggleContent>
        <toggleContent title="我忘記了會員帳號密碼，怎麼處理？">
            點擊"忘記密碼"按鈕，系統將發送重置密碼的連結至您的註冊郵箱，按照指示重設密碼。
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
    color:red
}
</style>