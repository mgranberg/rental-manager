<template>
  <div class="grid grid-flow-row gap-5">
    <CarCard v-for="car in store.getRentedCars" :car="car" />
  </div>
</template>

<script lang="ts" setup>
import { useCarsStore } from '~/stores/car.store';

const store = useCarsStore();
</script>

<style lang="scss" scoped>

</style>