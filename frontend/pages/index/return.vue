<template>
<div class="grid gap-5">
  <h1 class="text-xl">Rented cars</h1>
  <UDivider />
  <div class="grid grid-flow-row gap-5">
    <CarCard v-for="car in store.getRentedCars" :car="car" @return="startReturn(car)"/>
  </div>
  <UModal v-model="isOpen">
    <UCard>
      <template #header>
        <h1 class="text-3xl">Return car</h1>
        
      </template>

      <div class="p-4 grid gap-3">
        <UFormGroup label="Mileage">
          <UInput v-model="returnMileage" placeholder="Mileage" type="number" :min="carToReturn?.mileage"/>
        </UFormGroup>
      </div>

      <template #footer>
        <div class="flex gap-2 justify-between">
          <UButton color="primary" @click="returnCar()">Confirm return</UButton>
          <UButton color="red" @click="isOpen = false">Cancel</UButton>
        </div>
      </template>
    </UCard>
  </UModal>
</div>
</template>

<script lang="ts" setup>
import { useCarsStore } from '~/stores/car.store';
import type { Car } from '~/types/Car.type';
const isOpen = ref(false);
const carToReturn = ref<Car | null>(null);
const store = useCarsStore();
const returnMileage = ref(0);

const startReturn = (car: Car) => {
  isOpen.value = true;
  carToReturn.value = car;
  returnMileage.value = carToReturn.value.mileage;
}
const returnCar = async () => {
  if (!carToReturn.value) return;
  const returnData = {
    returnMileage: returnMileage.value
  }
  await $fetch(`/api/return`, {
    method: 'post',
    body: {
      identifier: carToReturn.value.bookingId,
      data: returnData
    }
  }).then(() => {
    isOpen.value = false;
  });
}
</script>

<style lang="scss" scoped>

</style>