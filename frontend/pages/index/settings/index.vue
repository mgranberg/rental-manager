<template>
  <div class="grid gap-5">
    <UCard>
      <template #header>
        <h1 class="text-3xl">
          Car types
        </h1>      
      </template>
      <div>
      <UTable 
        :rows="cartypes"
        :columns="carTypeColumns"
        v-if="cartypes" 
        :empty-state="{ icon: 'i-heroicons-circle-stack-20-solid', label: 'No items.' }"
        >
        <template #actions-data="{ row }">
          <UDropdown :items="items(row)">
            <UButton color="gray" variant="ghost" icon="i-heroicons-ellipsis-horizontal-20-solid" />
          </UDropdown>
        </template>
      </UTable>
      <UButton color="primary" @click="navigateTo('/settings/cartype/new')">Add new</UButton>
      </div>
  
    </UCard>
    <template v-if="settings">
      <UForm :state="settings" @submit="updateSettings">
      <UCard>
        <template #header>
          <h1 class="text-3xl">
            Settings
          </h1>      
        </template>
        <div class="grid grid-cols-2 gap-4" v-if="settings">
            <UFormGroup label="Base daily fee">
              <UInput v-model="settings.baseDailyFee" type="number" step="any" :min="1"/>
            </UFormGroup>
            <UFormGroup label="Base mileage fee">
              <UInput v-model="settings.kmFee" type="number" step="any" :min="1" />
            </UFormGroup>
          </div>
          <template #footer>
            <UButton color="primary" type="submit">Update</UButton>      
          </template>
        </UCard>
      </UForm>
    </template>
  </div>
</template>

<script lang="ts" setup>

import type { CarType } from '~/types/CarType.type';
import type { Settings } from '~/types/Settings.type';
const toast = useToast();
const items = (row: any) => [
  [{
    label: 'Edit',
    icon: 'i-heroicons-pencil-square-20-solid',
    click: async () => await navigateTo(`/settings/cartype/${row.id}`)
  }]
]
const updateSettings = async () => {
  await $fetch('/api/settings', {
    method: 'POST',
    body: settings.value
  }).then(() => {
    settingsRefresh();
    toast.add({ title: 'Settings Updated'});
  }).catch(e => {
    toast.add({ title: 'Error', description: e.message, color: 'red' });
  });
}
const { data: cartypes, status, error, refresh } = await useFetch<CarType[]>('/api/cartypes');
const { data: settings, status: settingsStatus, error: settingsError, refresh: settingsRefresh } = await useFetch<Settings>('/api/settings');
console.log(cartypes);
const carTypeColumns = [{
  key: 'name',
  label: 'Name'
}, {
  key: 'multiplier',
  label: 'Multiplier'
}, {
  key: 'multiplyDays',
  label: 'Multiply days'
}, {
  key: 'multiplyMileage',
  label: 'Multiply mileage'
}, {
  key: 'payForMileage',
  label: 'Pay for mileage'
},
{
  key: 'actions',
}
]

</script>

<style lang="scss" scoped>

</style>