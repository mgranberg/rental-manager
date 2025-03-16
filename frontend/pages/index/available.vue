<template>
  <div class="grid gap-5">
    <h1 class="text-xl flex justify-between">Available cars
    <UBadge size="lg" class="w-fit">Count: {{ availableCars?.length ?? 0 }}</UBadge></h1>
    
    <UDivider />

    <div class="grid grid-flow-row gap-5">
      <CarCard v-for="car in availableCars" :key="car.id" :car="car" @rent="rentCar(car)"></CarCard>
    </div>
  </div>
  <UModal v-model="isOpen" class="relative">
    <UButton icon="i-material-symbols-cancel-outline" class="absolute top-2 right-2" color="red" @click="abortRent"></UButton>
    <UCard>
      <div class="p-4 grid gap-3" v-if="bookingData.status === 'none' && bookingData.selectedCar">
        <h1 class="text-3xl">Confirm booking</h1>
        <UFormGroup label="Choose date">
          <VueDatePicker v-model="date" :range="{fixedStart: true}"></VueDatePicker>
        </UFormGroup>
        <UFormGroup label="Social security number">
          <UInput v-model="customerSsn" placeholder="YYMMDD-XXXX" />
        </UFormGroup>
      </div>
      <div v-if="bookingData.status == 'pending'" class="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center">
        Bokar...
      </div>
      <div v-if="bookingData.status === 'error'">
        <p>{{ bookingData.error }}</p>
      </div>
      <div v-if="bookingData.status === 'success'">
        <p>Bokning lyckades!</p>
      </div>
      <template #footer>
        <div class="flex gap-2 justify-between">
          <UButton color="primary" @click="onPublishBooking()" v-if="bookingData.status != 'success'">Bekräfta bokning</UButton>
          <UButton color="primary" @click="abortRent" v-if="bookingData.status === 'success'">Stäng</UButton>
          <UButton color="red" @click="isOpen = false" v-if="bookingData.status != 'success'">Avbryt</UButton>
        </div>
      </template>
    </UCard>
    </UModal>
</template>

<script lang="ts" setup>
import type { Car } from '~/types/Car.type';

const emit = defineEmits(['rent']);
const bookingData = ref({
  customerSsn: '',
  status: 'none' as 'none' | 'pending' | 'error' | 'success',
  error: '',
  selectedCar: null as Car | null
})
const isOpen = ref(false);

watch(isOpen, (val) => {
  if (!val) {
    abortRent();
  }
});
const startDate = new Date();
const endDate = new Date(new Date().setDate(startDate.getDate() + 2));
const date = ref([startDate, endDate]);


const { data: availableCars, status, error, refresh } = await useFetch<Car[]>('/api/availableCars');
console.log(availableCars);
console.log(error);



const rentCar = (car: Car) => {
  bookingData.value.selectedCar = car;
  isOpen.value = true;
}

const abortRent = () => {
  bookingData.value.selectedCar = null;
  bookingData.value.status = 'none';
  bookingData.value.error = '';
  bookingData.value.customerSsn = '';
  isOpen.value = false;
}

const customerSsn = ref('');

const onPublishBooking = async () => {
  bookingData.value.status = 'pending';
  await $fetch('api/bookings/', {
    method: 'POST',
    body: {
      carId: bookingData.value.selectedCar?.id,
      startDate: date.value[0],
      endDate: date.value[1],
      customerSsn: customerSsn.value
    }
  }).then(() => {
    bookingData.value.status = 'success';
    bookingData.value.selectedCar = null;
    bookingData.value.customerSsn = '';
    bookingData.value.error = '';
    emit('rent');
    refresh();
  }).catch((error) => {
    console.log(error);
    
    bookingData.value.status = 'error';
    bookingData.value.error = error;
  });
}
</script>

<style lang="scss" scoped>

</style>