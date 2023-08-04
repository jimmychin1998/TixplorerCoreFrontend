<template>
    <div class="margin">
        <h1 ref="myHeading" id="policy1">相關政策</h1>
        <h3>其他政策</h3>
        <toggleContent title="是否提供退款保證？">
            我們有提供7天未使用保證退款的保證，並且有相當完善的退貨流程及方式。
        </toggleContent>
        <toggleContent title="是否有兒童價或優惠？">
            提供兒童價或優惠政策，適用於特定年齡範圍的兒童，例如3歲至12歲。兒童價通常是成人價的一定比例折扣，客戶可以在訂購時選擇兒童價格，並在購票或預訂過程中填寫兒童的相關資訊。
        </toggleContent>
        <toggleContent title="是否有取消行程的政策？">
            提供取消行程的政策細節，例如客戶可以在出發前多少天內取消行程，並說明可能產生的取消費用。客戶可以通過網站、電話或客服中心進行取消申請，經過確認後，退款將根據取消政策退回至客戶的支付方式。
        </toggleContent>
        <toggleContent title="是否提供保險服務？">
            您可以在預訂時選擇是否需要購買旅遊保險。保險內容可能包括意外醫療保障、行李遺失補償等，客戶需支付相應的保險費用，以獲得保險保障。
        </toggleContent>
        <toggleContent title="是否接受團體訂購？">
            我們有些會接受團體預購，是否接受團體訂購，並說明團體訂購的人數要求和相關優惠政策。客戶可以在訂購過程中指定團體成員的人數和基本資訊，並享受團體價格折扣。團體訂購可能需要提前預定，以確保足夠的座位或住宿空間。
        </toggleContent>
        <br ref="myHeading" id="policy2">
        <br>
        <h3>隱私政策</h3>
        <toggleContent title="個人資料將如何被使用？">
            我們會使用在處理訂單、提供客戶支援、進行市場調查等，並且我們非常強調對客人個資的保護性。
        </toggleContent>
        <toggleContent title="個人資料是否會被保密？">
            我們向客戶保證個人資料將被妥善保密，並遵守政府相關的隱私保護法規。
        </toggleContent>
        <toggleContent title="個人資料如何管理和查閱？">
            您可以從會員資料中管理自己的個人資料以及查閱觀看。
        </toggleContent>
        <toggleContent title="個人資料如何保護">
            我們保護個人資料免於未經授權的訪問或洩露，用了以下幾種方式：
            1.數據加密：採用強大的加密技術，確保客戶的個人資料在傳輸和儲存過程中是加密的，即使被未授權的人訪問也難以解讀。
            <br>
            2.防火牆和安全系統：構建健全的防火牆和安全系統，監控網絡流量，以阻止未經授權的訪問和惡意攻擊。
            <br>
            3.訪問控制：限制對客戶個人資料的訪問權限，僅授予有必要訪問資料的員工，並實施多層次的身份驗證機制。
            <br>
            4.儲存安全：將客戶的個人資料儲存在安全受控的數據庫中，並定期進行數據備份，以防止數據丟失或損壞。
            <br>
            5.培訓員工：對員工進行相關的隱私保護培訓，強調個人資料的保密性，並教育他們辨識和應對安全風險。
            <br>
            6.監控和警報：建立監控機制，即時檢測和識別異常訪問行為，並及時發出警報，以迅速應對可能的安全威脅。
            <br>
            7.隱私政策：提供明確的隱私政策，告知客戶個人資料的使用目的和共享情況，讓客戶有信心並清楚知曉資料處理方式。
            <br>
            8.安全測試和審計：定期進行安全測試和審計，檢測系統中的漏洞，並及時修復和加強系統安全性。
            <br>
            9.細分許可權：根據工作職責，將許可權進行細分，確保只有授權人員能夠訪問特定類型的個人資料。
            <br>
            10.合規監管：確保遵守相關的隱私法規和標準，定期檢查和更新政策，以符合當地和國際的隱私保護要求。
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
    color:red;
}
</style>