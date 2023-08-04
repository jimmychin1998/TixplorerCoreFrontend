<template>
  <problemlist></problemlist>
  <problemsearch></problemsearch>
  <div class="container">
    <problemaccordion></problemaccordion>
    <problemclass></problemclass>
    <problemquestionsView :search-keyword="searchKeyword" />
  </div>
</template>
    
<script setup>
import problemlist from "../components/problemlist.vue";
import problemaccordion from "../components/problemaccordion.vue"
import problemsearch from "../components/problemsearch.vue";
import problemclass from "../components/problemclass.vue"
import { ref } from 'vue';
const searchKeyword = ref('');

$(document).ready(function () {
  $('.accordion-header').addClass('collapsed');
  $('.accordion-content').removeClass('show');
});

</script>
  
<style scoped></style>