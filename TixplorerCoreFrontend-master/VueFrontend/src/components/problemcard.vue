<template>
    <div class="margin">
        <h1 ref="myHeading" id="card1">付款類別</h1>
        <h3>付款方式</h3>
        <toggleContent title="有哪些付款方式可以使用？">
            我們提供多種付款方式，包括信用卡、轉帳、支付寶、PayPal、街口、LINEPAY...等。
        </toggleContent>
        <toggleContent title="我想使用信用卡付款，但系統顯示交易失敗，該怎麼辦？">
            建議客戶確保輸入的信用卡資訊正確無誤，並確認信用卡是否允許網上交易。如果問題仍然存在，建議客戶聯繫信用卡發卡銀行以瞭解交易失敗的原因。
        </toggleContent>
        <toggleContent title="我希望使用轉帳付款，應該如何操作？">
            提供轉帳付款的相關資訊，包括銀行帳戶號碼、收款人資料和轉帳的操作流程。
        </toggleContent>
        <toggleContent title="我有多個付款方式，可以同時使用嗎？">
            我們目前尚未支援多個付款方式，造成您的困擾，非常抱歉。
        </toggleContent>
        <toggleContent title="我不習慣在網上付款，還有其他付款方式嗎？">
            除了網上付款，我們也提供其他離線付款方式，例如ATM轉帳、現金付款或到門市辦理付款。
        </toggleContent>
        <br ref="myHeading" id="card2">
        <br>
        <h3>付款金額</h3>
        <toggleContent title="付款金額是否包含所有費用？">
            是的，確認付款金額包含所有費用，商品價格、服務費、稅費、運費等。
        </toggleContent>
        <toggleContent title="我想知道付款金額的明細，包括各項費用的分別。">
            點選我的訂單，我們訂單明細中有各式的付款金額、費用等明細。
        </toggleContent>
        <toggleContent title="是否可以使用優惠券或折扣碼來減少付款金額？">
            我們可以利用優惠券以及積分點數作為金額的折抵。
        </toggleContent>
        <toggleContent title="付款金額可以分期付款嗎？如果可以，有相關的利率和條件嗎？">
            因應政府政策，目前尚未提供分期付款的服務。
        </toggleContent>
        <toggleContent title="我已經付款，但訂單顯示未成功，怎麼處理？">
            客戶可以聯繫旅遊網站的客服，提供付款證明，並請求確認付款狀態，以解決訂單未成功的問題。
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