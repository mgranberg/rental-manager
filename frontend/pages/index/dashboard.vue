<template>
  <div class="grid grid-flow-row gap-5">
    <CarCard v-for="car in cars" :car="car" />
  </div>
</template>

<script lang="ts" setup>
const { data: cars, status, error, refresh } = await useFetch('/api/cars');
</script>

<style lang="scss" scoped>

</style>