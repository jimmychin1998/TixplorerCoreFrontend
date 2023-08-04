<template>
    <div class="margin">
        <h1 ref="myHeading" id="integral1">積分累積</h1>
        <h3>積分累積方式</h3>
        <toggleContent title="如何在旅遊中積累積分？">
            解答客戶在特定旅遊項目中如何獲得積分，例如預訂酒店、租車、搭乘航班或參加合作夥伴活動等。
        </toggleContent>
        <toggleContent title="積分是否有有效期限？如何保持積分不過期？">
            說明積分的有效期限，並提供保持積分有效的方法，例如定期使用積分或參與特定活動。
        </toggleContent>
        <toggleContent title="我在特定旅遊項目中沒有獲得預期的積分，該怎麼辦？">
            幫助客戶確認積分是否正確記錄，並提供聯繫相關旅遊公司或積分計畫客服的方式，以解決任何積分未到帳的問題。
        </toggleContent>
        <toggleContent title="是否可以在旅行後追加積分？例如忘記掃描積分卡或登記積分。">
            說明是否允許在旅行後追加積分，以及提供相應的程序和憑證要求。
        </toggleContent>
        <toggleContent title="我的積分顯示異常，為什麼積分增減不符合預期？">
            查詢客戶的積分紀錄，並解釋積分增減的原因，例如可能因為退款、取消訂單或其他促銷活動的影響。
        </toggleContent>
        <br ref="myHeading" id="integral2">
        <br>
        <h3>積分兌換與使用</h3>
        <toggleContent title="如何兌換積分以換取旅遊禮品或優惠券？">
            解釋兌換積分的程序，指導客戶如何使用積分換取旅遊禮品或優惠券。
        </toggleContent>
        <toggleContent title="積分是否可以轉贈給其他人使用？">
            說明積分是否可以轉讓，以及如何進行積分轉贈的程序。
        </toggleContent>
        <toggleContent title="我已經換取了旅遊禮品，但一直未收到，該怎麼辦？">
            確認旅遊禮品的發送情況，若發現問題，協助客戶聯繫相關單位或重新安排發送。
        </toggleContent>

        <toggleContent title="在兌換旅遊禮品時，系統顯示積分不足，但應該是足夠的，怎麼處理？">
            幫助客戶查明積分不足的原因，可能是系統錯誤或計算方式有誤，並確保客戶可以正確兌換所需禮品。
        </toggleContent>
        <toggleContent title="兌換的旅遊禮品有質量問題，可以辦理退換嗎？">
            指導客戶辦理退換程序，可能需要提供相關照片作為證據，然後安排換貨或退款。
        </toggleContent>
        <br ref="myHeading" id="integral3">
        <br>
        <h3>積分計畫相關問題</h3>
        <toggleContent title="我不小心重複下單了，希望取消其中一個訂單並退款。">
            客服人員會查詢您的重複訂單，然後幫助您取消其中一個訂單並辦理退款。
        </toggleContent>
        <toggleContent title="我可以加入積分計畫嗎？如何註冊？">
            提供註冊積分計畫的方式和步驟，並確保客戶順利加入。
        </toggleContent>
        <toggleContent title="是否有不同等級的積分會員，不同等級有什麼優惠？">
            解釋不同等級的積分會員對應的特權和優惠，例如積分倍數、專屬折扣等。
        </toggleContent>
        <toggleContent title="我可以同時參加多個積分計畫嗎？">
            說明是否允許同時參加多個積分計畫，並提供可能的注意事項和影響。
        </toggleContent>
        <toggleContent title="積分計畫是否有相關的服務熱線或客服信箱可以聯繫？">
            提供積分計畫相關的聯繫方式，以便客戶能夠隨時聯繫客服解決問題。
        </toggleContent>
        <toggleContent title="積分計畫是否有期限，我需要定期激活積分嗎？">
            說明積分計畫的相關規定，確保客戶了解積分計畫的有效期限和激活條件。
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