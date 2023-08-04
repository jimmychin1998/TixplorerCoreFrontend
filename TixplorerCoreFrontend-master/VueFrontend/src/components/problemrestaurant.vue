<template>
    <div class="margin">
        <h1 ref="myHeading" id="restaurant1">飯店訂房</h1>
        <h3>飯店選擇</h3>
        <toggleContent title="有哪些飯店可供選擇？我可以在哪些地區找到合適的飯店？">
            可以從我們提供的飯店列表和搜尋功能，搜尋到您去的地區合適的飯店選擇
        </toggleContent>
        <toggleContent title="我希望知道每家飯店的設施和服務，例如有無免費早餐、網路、健身房等。">
            在飯店資訊頁面詳細列出每家飯店的設施和服務，讓客戶可以方便地比較不同飯店的特點。
        </toggleContent>
        <toggleContent title="我對某家特定的飯店有疑問，可以提前查詢飯店相關問題嗎？">
            我們有提供飯店聯繫方式，讓您可以直接與飯店聯繫，詢問任何有關飯店的疑問。
        </toggleContent>
        <toggleContent title="我希望了解飯店附近的交通和便利設施，例如公共交通、超市等。">
            可以利用我們的地圖搜尋功能查看飯店相關的交通和便利設施。
        </toggleContent>
        <toggleContent title="有哪些飯店提供兒童友善的設施或親子活動？">
            我們在飯店資訊頁面標示提供兒童友善設施或親子活動的飯店，讓家庭客戶更容易找到合適的住宿選擇。
        </toggleContent>
        <br ref="myHeading" id="restaurant2">
        <br>
        <h3>預定問題</h3>
        <toggleContent title="我希望知道預定飯店的流程和步驟。">
            1.選擇日期和地點：客戶在網站或應用程式上選擇入住日期和退房日期，並選擇旅遊目的地或地點。
            <br>
            2.搜尋飯店：系統根據客戶選擇的日期和地點，顯示符合條件的飯店列表。客戶可以使用篩選器來進一步縮小搜索範圍，例如價格範圍、評價等。
            <br>
            3.選擇飯店：客戶從飯店列表中選擇一家合適的飯店，點擊詳情頁面以獲取更多飯店資訊。
            <br>
            4.填寫客戶資訊：客戶需要填寫預定時所需的資訊，例如姓名、聯繫方式、電子郵件地址等。有些網站可能也會要求提供信用卡資訊或其他付款方式。
            <br>
            5.確認訂單：客戶在確認預定資訊之前，可以再次檢查所選擇的日期、飯店、入住人數等資訊是否正確無誤。
            <br>
            6.選擇付款方式：客戶選擇合適的付款方式進行支付，例如信用卡、轉帳、支付寶等。在此步驟中，客戶也可以輸入優惠券或折扣碼（如適用）來抵扣訂單金額。
            <br>
            7.支付訂金或全額：根據飯店預定政策，客戶可能需要支付全額或訂金來確認預定。
            <br>
            8.完成預定：支付成功後，系統將顯示預定確認頁面，其中包含預定確認號碼和相關預定資訊。
            <br>
            9.收取預定確認函：客戶將收到預定確認的電子郵件或簡訊，其中包含預定確認號碼和入住資訊。
            <br>
            10.入住飯店：在預定日期到來時，客戶前往飯店並提供預定確認號碼或身份證明，完成入住手續。
        </toggleContent>
        <toggleContent title="我需要提前辦理退房手續嗎？退房流程是怎樣的？">
            1.了解退房時間：客戶在預定期間或入住時，應詢問飯店的退房時間。通常，退房時間在早上11點或中午12點。
            <br>
            2.準備退房：在退房當天，客戶應提前整理行李，確保所有個人物品已收拾好。
            <br>
            3.辦理退房手續：在退房時間前，客戶前往飯店櫃檯辦理退房手續。在櫃檯，客戶需要提供預定確認號碼或身份證明來證明入住身份。
            <br>
            4.結清費用：在櫃檯辦理退房手續時，客戶需要支付未付清的費用，例如未結清的房費、餐飲消費等。
            <br>
            5.歸還房卡或門卡：客戶應在退房時將房卡或門卡交還給櫃檯，確保不再擁有進入房間的權限。
            <br>
            6.檢查房間狀況：飯店工作人員可能會檢查客房的狀況，確保客房無損壞或物品遺留。
            <br>
            7.確認退房資訊：在完成退房手續後，飯店會提供退房收據或確認文件，確認客戶已完成退房手續並結清相關費用。
            <br>
            8.離開飯店：客戶在完成退房手續後，應盡快離開飯店，釋放房間供下一位客人使用。
        </toggleContent>
        <toggleContent title="我在入住期間遇到問題，需要客房服務或維修，該怎麼辦？">
            我們有提供客房服務或維修服務的聯繫方式，讓您可以及時聯繫飯店處理問題。
        </toggleContent>
        <toggleContent title="我想知道退房後是否可以存放行李？有無行李寄放服務？">
            在每個店家中如果有行李寄放服務會特地標註出來讓您看見。每個店家的收費方式不一詳情請看飯店資訊。
        </toggleContent>
        <toggleContent title="我在退房時忘記拿走私人物品，可以協助寄回嗎？">
            那是當然，詳細地留下您遺失的物品以及信息，我們會連絡飯店替您找尋。
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