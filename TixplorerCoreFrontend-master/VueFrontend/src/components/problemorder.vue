<template>
        <div class="margin">
                <h1 ref="myHeading" id="order1">訂單問題</h1>
                <h3>訂單狀態</h3>
                <toggleContent title="我已經訂購了一個旅遊套餐，但還沒收到訂單確認通知。訂單是否已經成功？">
                        請確認你提供的郵件地址是否正確，並檢查垃圾郵件或垃圾信箱。如果還是找不到確認通知，請聯繫客服以確認訂單狀態。
                </toggleContent>
                <toggleContent title="我的旅遊訂單顯示為'處理中'已經有一段時間了，我想知道進展情況。">
                        請耐心等待一段時間，有時訂單處理需要一些時間。如果你覺得等待時間過長，你可以聯繫客服查詢訂單進度。
                </toggleContent>
                <toggleContent title="我錯誤地選擇了錯誤的出發日期，我該如何更改訂單？">
                        客服人員可以幫助您更改訂單中的日期，或者重新預定新的日期，並確保修改後的訂單詳情是正確的。
                </toggleContent>
                <toggleContent title="我收到了部分商品，但訂單狀態顯示為已完成。還有其他商品未發貨嗎？">
                        客服人員會查詢您的訂單，並確認剩餘商品的發貨情況，或者提供任何有關運送的相關信息。
                </toggleContent>
                <br ref="myHeading" id="order2">
                <h3>付款問題</h3>
                <toggleContent title="我的付款未成功，但是信用卡顯示已被扣款。怎麼處理">
                        客服人員會查證付款狀態，如果確認付款未成功，將協助您處理退款事宜。
                </toggleContent>
                <toggleContent title="我想更改付款方式，該怎麼辦？">
                        客服人員會指導您如何更改付款方式，或者協助您重新進行付款。
                </toggleContent>
                <toggleContent title="我選擇了銀行轉帳付款，但找不到付款相關的資訊，我應該向誰查詢？">
                        客服人員可以提供銀行轉帳付款的相關資訊，包括帳戶號碼和收款方的資料。
                </toggleContent>
                <toggleContent title="我支付了保證金，但不確定是否成功預訂。如何確認？">
                        客服人員可以查詢您的保證金付款狀態，並確認預訂是否已成功。
                </toggleContent>
                <br ref="myHeading" id="order3">
                <h3>退換貨</h3>
                <toggleContent title="我收到的商品有損壞或缺失，我應該如何辦理退換貨？">
                        客服人員會指導您辦理退換貨的流程，包括提交照片作為證據，然後安排換貨或退款。
                </toggleContent>
                <toggleContent title="我更改了行程，需要取消部分商品並辦理退款，應該怎麼處理？">
                        客服人員會指導您辦理取消訂單並退款的程序，可能涉及相應的取消費用。
                </toggleContent>
                <toggleContent title="我不小心重複下單了，希望取消其中一個訂單並退款。">
                        客服人員會查詢您的重複訂單，然後幫助您取消其中一個訂單並辦理退款。
                </toggleContent>
                <toggleContent title="我換主意不想要這個商品了，可以辦理退貨嗎？">
                        客服人員會告知您的商品是否適用於退貨，並指導您辦理退貨程序和退款事宜。
                </toggleContent>
                <br ref="myHeading" id="order4">
                <h3>取消訂單</h3>
                <toggleContent title="我需要取消整個行程，應該如何辦理取消訂單？">
                        客服人員會指導您辦理取消整個行程的程序，可能涉及取消費用和退款事宜。
                </toggleContent>
                <toggleContent title="我已經付款，但因為突發情況需要取消部分行程，可以退還部分費用嗎？">
                        客服人員會告知您部分行程的取消政策和可能的退款金額。
                </toggleContent>
                <toggleContent title="我嘗試聯繫多次取消訂單，但沒有得到回覆，該怎麼辦？">
                        客服人員會確認您的取消請求，並儘快協助您辦理取消訂單手續。
                </toggleContent>
                <toggleContent title="我不小心刪除了確認郵件，無法找到訂單詳情，怎麼辦？">
                        客服人員可以重新寄送確認郵件，或者提供您所需的訂單詳情。
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
        const navbarHeight = document.querySelector('.sticky-top').offsetHeight;
        const targetPosition = targetElement.getBoundingClientRect().top + window.scrollY;
        const offsetPosition = targetPosition - navbarHeight + 100; // 調整這個數值來控制滾動位置
        window.scrollTo({ top: offsetPosition, behavior: 'smooth' });
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
        margin-top: 60px;
        margin-bottom: 20px;
        color:red
}
</style>