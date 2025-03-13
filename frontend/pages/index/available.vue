<template>
  <section class="mb-4">
    <UFormGroup label="Filtrera på datum">
      <VueDatePicker v-model="date" :range="{fixedStart: true}"></VueDatePicker>
    </UFormGroup>
  </section>

  <section class="grid grid-flow-row gap-4">

    <UCard v-if="status === 'pending'">
      <p>Laddar...</p>
    </UCard>
    <UCard v-else-if="status === 'error'">
      <p>{{ error }}</p>
    </UCard>
    <CarCard v-else v-for="car in availableCars" :key="car" :car="car" @rent="rentCar(car)"></CarCard>
  </section>
  <UModal v-model="isOpen">
    <UCard>
      <div class="p-4" v-if="bookingData.status === 'none' && bookingData.selectedCar">
        <h1 class="text-3xl">Bekräfta bokning</h1>
        <p class="text-sm">Du har valt att boka en {{ bookingData.selectedCar?.make }} {{ bookingData.selectedCar?.model }} från {{ date[0] }} till {{ date[1] }}.</p>
        <UFormGroup label="Personnummer">
          <UInput v-model="customerSsn" placeholder="YYYYMMDD-XXXX" />
        </UFormGroup>
        <UButton color="primary" @click="onPublishBooking()" >Bekräfta bokning</UButton>
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
        <div class="flex gap-2">
          <UButton color="red" @click="isOpen = false">Avbryt</UButton>
        </div>
      </template>
    </UCard>
    </UModal>
</template>

<script lang="ts" setup>
import type { Car } from '~/Types/Car.type';
const bookingData = ref({
  customerSsn: '',
  status: 'none' as 'none' | 'pending' | 'error' | 'success',
  error: '',
  selectedCar: null as Car | null
})
const isOpen = ref(false);
const startDate = new Date();
const endDate = new Date(new Date().setDate(startDate.getDate() + 2));
const date = ref([startDate, endDate]);
const { data: availableCars, status, error, refresh } = await useFetch('api/availableCars/', {
  method: 'POST',
  body: {
    startDate: date.value[0],
    endDate: date.value[1]
  },
  watch: [date]
})

const rentCar = (car: Car) => {
  bookingData.value.selectedCar = car;
  isOpen.value = true;
}

const customerSsn = ref('');

const onPublishBooking = async () => {
  bookingData.value.status = 'pending';
  await $fetch('api/bookings/', {
    method: 'POST',
    body: {
      car: bookingData.value.selectedCar,
      startDate: date.value[0],
      endDate: date.value[1],
      customerSsn: customerSsn.value
    }
  }).then(() => {
    bookingData.value.status = 'success';
    bookingData.value.selectedCar = null;
    bookingData.value.customerSsn = '';
    bookingData.value.error = '';
    refresh();
  }).catch((error) => {
    bookingData.value.status = 'error';
    bookingData.value.error = error;
  })
}
</script>

<style lang="scss" scoped>

</style>