<template>
  <div class="flex min-h-screen">
    <aside class="w-1/4 min-h-screen bg-gray-200 p-4">
      <h1 class="text-lg font-bold mb-4">Rent Manager</h1>
      <UVerticalNavigation :links="menuItems" />
    </aside>
    <main class="w-3/4 p-6">
      <NuxtPage />
  
    </main>
  </div>
</template>

<script lang="ts" setup>
const menuItems = [
  {
    label: 'Dashboard',
    icon: 'i-tabler-home',
    to: '/dashboard'
  },
  {
    label: 'Available cars',
    icon: 'i-uil-car-sideview',
    to: '/available'
  },
  {
    label: 'Return',
    icon: 'i-ph-key-return',
    to: '/return'
  },
]

definePageMeta({
  title: 'Rent Manager',
  redirect: '/dashboard'
})
</script>

<style lang="scss" scoped>

</style>