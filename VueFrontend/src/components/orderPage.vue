<template>
    <div class="d-flex justify-content-center align-items-center">
        <nav aria-label="Page navigation example">
            <ul class="pagination">
                <li class="page-item" v-for="(value, index) in totalPage" :key="index" @click="clickHander(value)"><a
                        class="page-link" href="#">{{
                            value }}</a></li>
            </ul>
        </nav>
    </div>
</template>
    
<script setup>
const props = defineProps({
    totalPage: Number
})
const emit = defineEmits('childclick')
const clickHander = page => {
    emit('childclick', page)
}
//console.log(props.totalPage)

</script>
    
<style scoped></style>