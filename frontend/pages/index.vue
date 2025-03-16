<template>
  <div class="flex min-h-screen">
    <aside class="w-1/4 h-screen bg-gray-200 p-4 sticky top-0">
      <h1 class="text-lg font-bold mb-4">Rent Manager</h1>
      <UVerticalNavigation :links="menuItems" />
    </aside>
    <main class="w-3/4 p-6">
      <NuxtPage />
    </main>
  </div>
</template>

<script lang="ts" setup>
import { useCarsStore } from '~/stores/car.store';
import type { Car } from '~/types/Car.type';

const menuItems = [[
  {
    label: 'Available cars',
    icon: 'i-uil-car-sideview',
    to: '/available'
  },
  {
    label: 'Return',
    icon: 'i-tabler-home',
    to: '/return'
  }],
  [
    {
      label: 'Settings',
      icon: 'i-uil-cog',
      to: '/settings'
    }],
    [{
      label: 'receipts',
      icon: 'i-uil-cog',
      to: '/receipt'
    }]
]

definePageMeta({
  title: 'Rent Manager',
  redirect: '/Available'
})
</script>

<style lang="scss" scoped>

</style>