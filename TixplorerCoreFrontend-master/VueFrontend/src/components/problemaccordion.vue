<template>
  <h2 class="text-center  mt-5">常見問題</h2>
  <div class="accordion" id="myAccordion">
    <div class="accordion-item">
      <h2 class="accordion-header" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true">
        點數該如何做使用呢?
        <span class="accordion-indicator">&#9662;</span>
      </h2>
      <div id="collapseOne" class="accordion-content collapse show">
        <div class="accordion-body">
          我們的點數系統讓您能夠在購物、預訂服務或參加活動時獲得積分，積攢的積分可以用於享受更多的優惠和福利。以下是如何使用點數的步驟：
          <br>
          1.登入您的帳戶，並進入「我的點數」頁面。
          <br>
          2.查看您的積分餘額，了解您當前的積分情況。
          <br>
          3.在結帳頁面或預訂服務頁面，選擇使用點數支付，並輸入您希望使用的積分數量。
          <br>
          4.確認訂單後，相應的積分將會自動抵扣訂單金額。
          <br>
          5.您可以隨時在「我的點數」頁面查看使用記錄和積分餘額。
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false">
        該如何做退款呢?
        <span class="accordion-indicator">&#9662;</span>
      </h2>
      <div id="collapseTwo" class="accordion-content collapse">
        <div class="accordion-body">
          當您需要進行退款時，我們將確保為您提供便捷、快速的退款流程。請按照以下步驟進行退款：
          <br>
          1.登入您的帳戶，並進入訂單頁面。
          <br>
          2.找到您需要退款的訂單，並點擊「申請退款」按鈕。
          <br>
          3.填寫退款申請表格，包括退款原因和希望退款的金額。
          <br>
          4.提交退款申請後，我們的客服團隊將盡快審核您的申請。
          <br>
          5.一旦審核通過，您將收到一封確認退款的電子郵件，並在數個工作日內收到退款金額。
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false">
        如何查詢所需位置?
        <span class="accordion-indicator">&#9662;</span>
      </h2>
      <div id="collapseThree" class="accordion-content collapse">
        <div class="accordion-body">
          想要查詢特定位置的方法非常簡單。您可以通過以下途徑輕鬆找到所需位置：
          <br>
          1.使用我們的線上地圖服務：在我們的網站或手機應用上，使用地圖功能搜索您需要的地點或地址，我們會為您提供準確的導航和定位。
          <br>
          2.聯繫客服尋求幫助：如果您對地點的位置不太熟悉，您可以隨時聯繫我們的客服團隊，他們會為您提供詳細的位置信息和路線指引。
          <br>
          3.查閱指南或地點信息：在我們的網站或應用上，您還可以查閱相關地點的指南或詳細信息，包括地址、聯繫方式、營業時間等，以幫助您更好地找到目的地。
        </div>
      </div>
    </div>
    <div class="accordion-item">
      <h2 class="accordion-header" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false">
        如何聯繫我們客服
        <span class="accordion-indicator">&#9662;</span>
      </h2>
      <div id="collapseFour" class="accordion-content collapse">
        <div class="accordion-body">
          客服電話：0977-888-999
          <br>
          客服聯絡時間：
          <br>
          周一至周五 09:00~20:00
          <br>
          周六至周日 09:00~18:00
        </div>
      </div>
    </div>
  </div>
</template>
    
<script setup>

</script>
    
<style scoped>
.accordion-item {
  margin-bottom: 10px;
}

.accordion-header {
  background-color: #f8f9fa;
  padding: 10px;
  cursor: pointer;
  position: relative;
}

.accordion-header:hover {
  background-color: #e9ecef;
}

.accordion-content {
  background-color: #ffffff;
  padding: 10px;
}

.accordion-indicator {
  position: absolute;
  top: 50%;
  right: 10px;
  transform: translateY(-50%);
  transition: transform 0.3s;
}

.accordion-header.collapsed .accordion-indicator {
  transform: translateY(-50%) rotate(-180deg);
}
</style>