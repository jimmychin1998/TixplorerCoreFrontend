<template>
    <div id="divsearch" class="search-container">
        <h2>快速搜尋</h2>
        <div class="search-wrapper">
            <input type="text" class="search-input" placeholder="有想搜尋甚麼問題嗎?" v-model="searchKeyword"     @input="filterOptions">
            <button class="search-button" @click="handleSearch">搜尋</button>
        </div>
        <ul class="search-options" v-show="showOptions">
        <li v-for="option in filteredOptions" :key="option" @click="selectOption(option)">{{ option }}</li>
      </ul>

    </div>
</template>
    
<script setup>
import { ref, defineEmits,computed } from 'vue';
import { useRouter } from 'vue-router';
const router = useRouter();
const searchKeyword = ref('');
const emit = defineEmits('search')
const showOptions = ref(false);
const options = ["我已經訂購了一個旅遊套餐，但還沒收到訂單確認通知。訂單是否已經成功？", "我的旅遊訂單顯示為'處理中'已經有一段時間了，我想知道進展情況。", "我錯誤地選擇了錯誤的出發日期，我該如何更改訂單？", "我收到了部分商品，但訂單狀態顯示為已完成。還有其他商品未發貨嗎？", "我的付款未成功，但是信用卡顯示已被扣款。怎麼處理","我想更改付款方式，該怎麼辦？","我選擇了銀行轉帳付款，但找不到付款相關的資訊，我應該向誰查詢？","我支付了保證金，但不確定是否成功預訂。如何確認？","我收到的商品有損壞或缺失，我應該如何辦理退換貨？","我更改了行程，需要取消部分商品並辦理退款，應該怎麼處理？","我不小心重複下單了，希望取消其中一個訂單並退款。","我換主意不想要這個商品了，可以辦理退貨嗎？","我需要取消整個行程，應該如何辦理取消訂單？","我已經付款，但因為突發情況需要取消部分行程，可以退還部分費用嗎？","我嘗試聯繫多次取消訂單，但沒有得到回覆，該怎麼辦？","我不小心刪除了確認郵件，無法找到訂單詳情，怎麼辦？","如何在旅遊中積累積分？","積分是否有有效期限？如何保持積分不過期？","我在特定旅遊項目中沒有獲得預期的積分，該怎麼辦？","是否可以在旅行後追加積分？例如忘記掃描積分卡或登記積分。","我的積分顯示異常，為什麼積分增減不符合預期？","如何兌換積分以換取旅遊禮品或優惠券？","積分是否可以轉贈給其他人使用？","我已經換取了旅遊禮品，但一直未收到，該怎麼辦？","在兌換旅遊禮品時，系統顯示積分不足，但應該是足夠的，怎麼處理？","兌換的旅遊禮品有質量問題，可以辦理退換嗎？","我不小心重複下單了，希望取消其中一個訂單並退款。","我可以加入積分計畫嗎？如何註冊？","是否有不同等級的積分會員，不同等級有什麼優惠？","我可以同時參加多個積分計畫嗎？","積分計畫是否有相關的服務熱線或客服信箱可以聯繫？","積分計畫是否有期限，我需要定期激活積分嗎？","我試圖註冊帳號，但系統顯示我的電子郵件已被使用，該怎麼辦？","我嘗試註冊帳號時出現錯誤，無法完成註冊，該怎麼處理？","我已經註冊了帳號，但無法登錄，該怎麼處理？","是否可以使用社交媒體帳號註冊？如果可以，如何操作？","我希望註冊帳號時保持匿名，是否可以註冊無需提供個人資料的帳號？","是否可以使用社交媒體帳號註冊？如果可以，如何操作？","我收到一封來自不明來源的電子郵件，要求我提供帳號資訊，這是否合法？","我擔心我之前使用的密碼太弱，該如何更改密碼？","我擔心我的帳號安全，有什麼方法可以增強帳號的安全性？","我希望了解該旅遊網站的帳號資料是否會被共用或出售給第三方？","我想知道如果我的帳號遭到黑客攻擊或資料外洩，該怎麼處理？","有哪些付款方式可以使用？","是否可以使用社交媒體帳號註冊？如果可以，如何操作？","我想使用信用卡付款，但系統顯示交易失敗，該怎麼辦？","我希望使用轉帳付款，應該如何操作？","我有多個付款方式，可以同時使用嗎？","我不習慣在網上付款，還有其他付款方式嗎？","付款金額是否包含所有費用？", "我想知道付款金額的明細，包括各項費用的分別。","是否可以使用優惠券或折扣碼來減少付款金額？","付款金額可以分期付款嗎？如果可以，有相關的利率和條件嗎？","我已經付款，但訂單顯示未成功，怎麼處理？","有哪些飯店可供選擇？我可以在哪些地區找到合適的飯店？","我希望知道每家飯店的設施和服務，例如有無免費早餐、網路、健身房等。","我對某家特定的飯店有疑問，可以提前查詢飯店相關問題嗎？","我希望了解飯店附近的交通和便利設施，例如公共交通、超市等。","有哪些飯店提供兒童友善的設施或親子活動？","我希望知道預定飯店的流程和步驟。","我需要提前辦理退房手續嗎？退房流程是怎樣的？","我在入住期間遇到問題，需要客房服務或維修，該怎麼辦？","我想知道退房後是否可以存放行李？有無行李寄放服務？","我在退房時忘記拿走私人物品，可以協助寄回嗎？","我該如何申請成為會員？","會員資格是否需要付費？","是否有年齡限制或其他條件成為會員？","我可以申請家庭會員資格嗎？","會員專屬優惠有哪些？","怎麼查看我的會員專屬優惠？","會員優惠是否適用於所有預訂？","我忘記了會員帳號密碼，怎麼處理？","我在哪裡可以找到可用的優惠代碼？", "優惠代碼可以用在哪些產品或服務上？","我該如何使用優惠代碼？","為什麼我的優惠代碼無效？","我可以同時使用多個優惠代碼嗎？","優惠代碼是否有使用限制或有效期限？","我錯過了優惠代碼發放的活動，還有其他方式可以獲得優惠嗎？","有哪些不同的旅遊方式可供選擇？","如何選擇適合我的旅遊方式？", "自由行和跟團旅遊有何不同？","我該選擇哪種交通方式來前往目的地？","交通方式的票價和預訂方式是什麼？", "是否提供接送服務？","交通方式對行李的限制是什麼？","旅遊行程中是否有導遊服務？","導遊費用包含在旅遊費用中嗎？","導遊會提供哪些服務？","是否可以自行安排行程，不需要導遊？","是否提供退款保證？","是否有兒童價或優惠？","是否有取消行程的政策？","是否提供保險服務？","是否接受團體訂購？", "個人資料將如何被使用？","個人資料是否會被保密？","個人資料如何管理和查閱？","個人資料如何保護",];
const handleSearch = () => {
    const keyword = searchKeyword.value;
    emit('search', keyword);
    router.push({ name: 'problemquestion', query: { searchKeyword: keyword } });
};
const filteredOptions = computed(() => {
  const keyword = searchKeyword.value.toLowerCase();
  const uniqueOptions = new Set();
  options.forEach(option => {
    if (option.toLowerCase().includes(keyword)) {
      uniqueOptions.add(option);
    }
  });
  return Array.from(uniqueOptions);
});
const filterOptions = () => {
  showOptions.value = searchKeyword.value.trim() !== '';
};

const selectOption = (option) => {
  searchKeyword.value = option;
  showOptions.value = false;
  clickSearchButton(); // 點擊搜尋按鈕的方法
};

const clickSearchButton = () => {
  const searchButton = document.querySelector(".search-button");
  if (searchButton) {
    searchButton.click(); // 模擬點擊搜尋按鈕
  }
};
filterOptions()
</script>
    
<style scoped>
#divsearch {
    background-color: rgb(218, 234, 226);
    height: 20vh;
    margin-top: 20px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.search-wrapper {
    display: flex;
    align-items: center;
    width: 50%;
    justify-content: center;

}

.search-container {
    display: flex;
    align-content: center;
    justify-content: center;
    width: 100%;
    margin: auto;
}

.search-input {
    flex: 1;
    border: none;
    padding: 10px;
    font-size: 16px;
    border-radius: 4px 0 0 4px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    background-color: #fff;
}

.search-button {
    padding: 10px 20px;
    background-color: #ff4081;
    color: white;
    border: none;
    border-radius: 0 4px 4px 0;
    cursor: pointer;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
.search-options {
    list-style: none;
  padding: 0;
  margin: 0;
  position: absolute;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 4px;
  width: calc(50% - 70px);
  max-height: 150px;
  overflow-y: auto;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  z-index: 1; /* Ensure the options appear above other elements */
  top: 27%; /* Position the options below the input */
  left: 25%; /* Align the options with the left edge of the input */
  transform: translateY(5px);
}

.search-options li {
  padding: 8px 10px;
  cursor: pointer;
}

.search-options li:hover {
  background-color: #f2f2f2;
}
</style>