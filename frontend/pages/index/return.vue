<template>
<div class="grid gap-5">
  <h1 class="text-xl">Rented cars</h1>
  <UDivider />
  <div class="grid grid-flow-row gap-5">
    <CarCard v-for="car in data" :car="car" @return="startReturn(car)"/>
    <template v-if="data?.length === 0">
        <h2 class="text-xl">No cars rented</h2>
    </template>
  </div>
  <UModal v-model="isOpen">
    <UForm @submit="returnCar" :state="state">
      <UCard>
        <template #header>
          <h1 class="text-3xl">Return car</h1>
          
        </template>
  
        <div class="p-4 grid gap-3">
          <UFormGroup label="Mileage">
            <UInput v-model="state.returnMileage" placeholder="Mileage" type="number" :min="carToReturn?.mileage"/>
          </UFormGroup>
        </div>
  
        <template #footer>
          <div class="flex gap-2 justify-between">
            <UButton color="primary" type="submit">Confirm return</UButton>
            <UButton color="red" @click="isOpen = false">Cancel</UButton>
          </div>
        </template>
      </UCard>
    </UForm>
  </UModal>
</div>
</template>

<script lang="ts" setup>
import { useCarsStore } from '~/stores/car.store';
import type { Car } from '~/types/Car.type';
const isOpen = ref(false);
const carToReturn = ref<Car | null>(null);
const store = useCarsStore();
const state = reactive({
  returnMileage: 0
})
const toast = useToast();
const { data, status, error, refresh } = await useFetch('/api/cars',{
    transform: (data: Car[]) => {
        return data.filter(car => car.bookingId);
    }
})

const startReturn = (car: Car) => {
  isOpen.value = true;
  carToReturn.value = car;
  state.returnMileage = carToReturn.value.mileage;
}
const returnCar = async () => {
  if (!carToReturn.value) return;
  const returnData = {
    returnMileage: state.returnMileage,
    identifier: carToReturn.value.bookingId?.toString() ?? '',
    isId: true
  }
  await $fetch(`/api/return`, {
    method: 'post',
    body: returnData
  }).then(async (res: any) => {
    await navigateTo('/receipt/' + res.bookingNumber);
  });
}
</script>

<style lang="scss" scoped>

</style>