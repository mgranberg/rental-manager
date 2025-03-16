<template>
  <div>
    <ul class="space-y-4">
      <li v-for="booking in data" :key="booking.bookingNumber">
        <router-link :to="`/receipt/${booking.bookingNumber}`">
          <UCard>
            {{ booking.bookingNumber }}
          </UCard>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts" setup>
const { data, status, error, refresh } = await useFetch<any>('/api/bookings');
</script>

<style lang="scss" scoped>

</style>