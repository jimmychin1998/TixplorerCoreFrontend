<template>
    <div class="margin">
        <h1 ref="myHeading" id="discount1">優惠代碼</h1>
        <h3>優惠代碼使用</h3>
        <toggleContent title="我在哪裡可以找到可用的優惠代碼？">
            優惠代碼通常在官方網站、電子郵件訂閱、社群媒體頁面或合作夥伴網站上發布。客戶可經常查看相關平台以尋找可用的優惠代碼。
        </toggleContent>
        <toggleContent title="優惠代碼可以用在哪些產品或服務上？">
            說明優惠代碼適用的產品範疇或服務類別，例如特定飯店、旅遊套餐、機票預訂等。
        </toggleContent>
        <toggleContent title="我該如何使用優惠代碼？">
            在結帳頁面輸入優惠代碼，系統會自動套用相關折扣或優惠。
        </toggleContent>
        <toggleContent title="為什麼我的優惠代碼無效？">
            可能有幾個原因：優惠代碼已過期、優惠代碼僅適用於特定產品或服務、優惠代碼不正確或拼寫錯誤。客戶應仔細檢查並確認優惠代碼的條件。
        </toggleContent>
        <toggleContent title="我可以同時使用多個優惠代碼嗎？">
            一般情況下，系統只允許使用一個優惠代碼，客戶需選擇應用最適合的優惠代碼。
        </toggleContent>
        <br ref="myHeading" id="discount2">
        <br>
        <h3>優惠代碼發放</h3>
        <toggleContent title="優惠代碼是否有使用限制或有效期限？">
            是的，是有可能的，有些會只有第一次可使用，有些要達到某些金額才可使用。
        </toggleContent>
        <toggleContent title="優惠代碼是否可以轉讓他人使用？">
            通常優惠代碼是僅限持有者使用的，不可轉讓他人使用。
        </toggleContent>
        <toggleContent title="我錯過了優惠代碼發放的活動，還有其他方式可以獲得優惠嗎？">
            客戶可以關注官方網站或社群媒體，以獲得即將到來的促銷活動或優惠代碼發放的消息。
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