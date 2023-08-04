<template>
            
    <div class="photo-container mb-5">
      <div class="photo-text">
        <h2>需要什麼協助??</h2>
      </div>
    </div>
</template>
    
<script setup>
    
</script>
    
<style scoped>
    .photo-container {
  width: 100%;
  height: 0;
  padding-top: 8%; /* 設置高度為10% */
  position: relative;
  background-image: url('../assets/images/風景圖.jpg');
  background-size: cover;
  background-position: center top; /* 設置背景位置在頂部中間 */
}

.photo-text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-weight: bold !important;
  color: #fff !important;
  font-size:  24px !important;
  padding: 20px;
  border-radius: 8px;
}
</style>