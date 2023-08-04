<template>
    <div class="title" @click="toggleContent">
        <span>
            <img v-show="data.isOpen" src="../assets/images/arrowdown.png" alt="down-arrow" />
            <img v-show="!data.isOpen" src="../assets/images/arrowright.png" alt="left-arrow" />
        </span>
        <span v-html="title"></span>
    </div>
    <div class="content" ref="contentRef" v-show="data.isOpen">
        <div class="slotstyle">
            <slot></slot>
        </div>
    </div>
</template>
  
<script setup>
import { reactive, ref, defineProps } from 'vue';


const props = defineProps(['title']);

const data = reactive({
    isOpen: false
});

const contentRef = ref(null);

const toggleContent = () => {
    data.isOpen = !data.isOpen;
};
</script>
  
<style scoped>
.title {
    cursor: pointer;
    display: flex;
    align-items: center;
    margin-bottom: 20px;
}

.title span {
    margin-right: 5px;
    font-size: 24px;
}

.title span:hover {
    color: orange
}

.content {
    margin-bottom: 20px;
}

.slotstyle {
    background-color: #F0F0F0;
    font-size: 20px;
    border: 3px dashed red;
    padding: 10px;
}
</style>