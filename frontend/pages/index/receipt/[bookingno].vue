<template>
  <div>
    <div>
      BookingNo: {{ bookingno }}
    </div>
    <div>
      Customer SSN: {{ data?.customerSsn }}
    </div>
    <div>
      Car: {{ data?.car?.licensePlate }}
    </div>
    <div>
      Price: {{ data?.totalPrice }}
    </div>
    <div>
      Mileage: {{ data?.endingMileage - data?.startingMileage  }}
    </div>
    
  </div>
</template>

<script lang="ts" setup>
const route = useRoute();
const { bookingno } = route.params;
const { data, pending, error, refresh } = await useFetch<any>(`/api/bookings`, {
  params: {
    bookingNumber: bookingno
  }
});

</script>

<style lang="scss" scoped>

</style>