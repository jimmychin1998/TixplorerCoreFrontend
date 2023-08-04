<template>
    <div class="margin">
        <h1 ref="myHeading" id="journey1">行程方式</h1>
        <h3>旅遊方式選擇</h3>
        <toggleContent title="有哪些不同的旅遊方式可供選擇？">
            我們提供相當多的旅遊方式供您做選擇，例如自由行、跟團旅遊、包車遊覽、郵輪旅遊等。
        </toggleContent>
        <toggleContent title="如何選擇適合我的旅遊方式？">
            根據您的喜愛與需求，考慮時間、預算、行程內容等因素，選擇最適合的旅遊方式。
        </toggleContent>
        <toggleContent title="自由行和跟團旅遊有何不同？">
            自由行是自己安排行程和活動，跟團旅遊則由導遊帶領和安排行程，客戶可以根據個人喜好選擇。
        </toggleContent>
        <toggleContent title="是否有深度旅遊選項？">
            我們提供深度旅遊選項，例如生態旅遊、文化體驗、探險旅遊等，讓客戶有更多豐富的旅遊體驗。
        </toggleContent>
        <br ref="myHeading" id="journey2">
        <br>
        <h3>交通方式</h3>
        <toggleContent title="我該選擇哪種交通方式來前往目的地？">
            我們提供相當多的行程方式供您做選擇，如飛機快速但費用較高，火車舒適但時間較長等，
        </toggleContent>
        <toggleContent title="交通方式的票價和預訂方式是什麼？">
            我們網頁提供線上訂購以及預訂的服務，供您做參考選擇。
        </toggleContent>
        <toggleContent title="是否提供接送服務？">
            我們目前沒提供接送服務哦！
        </toggleContent>
        <toggleContent title="交通方式對行李的限制是什麼？">
            如是叫車的話看他們車的容量，而飛機有行李重量限制、火車有行李容量....等
        </toggleContent>
        <br ref="myHeading" id="journey3">
        <br>
        <h3>導遊服務</h3>
        <toggleContent title="旅遊行程中是否有導遊服務？">
            看您的需求，有導遊需求的話會在網頁資訊上告知您。
        </toggleContent>
        <toggleContent title="導遊費用包含在旅遊費用中嗎？">
            是的，導遊的費用是含在旅遊費用之中。
        </toggleContent>
        <toggleContent title="導遊會提供哪些服務？">
            導遊通常會提供導覽解說、行程安排、地陪導遊、景點導覽、多語言導遊、交通安排、餐飲建議、購物建議、問題解答...等
        </toggleContent>
        <toggleContent title="是否可以自行安排行程，不需要導遊？">
            當然可以，您可以自由選擇需不需要導遊，也可以自行安排行程。
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