<template>
  <div>
    <h1 class="text-xl">Edit car type</h1>
    <UCard>
      <template #header>
        <h1 class="text-3xl">Edit car type</h1>
      </template>
      
      <div class="p-4 grid gap-3" v-if="data">
        <UFormGroup label="Name">
          <UInput v-model="data.name"/>
        </UFormGroup>
        <UFormGroup label="Multiplier">
          <UInput v-model="data.multiplier" type="number" />
        </UFormGroup>
        <UFormGroup label="Multiply days">
          <UCheckbox v-model="data.multiplyDays" />
        </UFormGroup>
        <UFormGroup label="Multiply mileage">
          <UCheckbox v-model="data.multiplyMileage"/>
        </UFormGroup>
        <UFormGroup label="Pay for mileage">
          <UCheckbox v-model="data.payForMileage"></UCheckbox>
        </UFormGroup>
      </div>
      <template #footer>
        <div class="flex gap-2 justify-between">
          <UButton color="primary" @click="update">Uppdatera</UButton>
          <UButton color="red" @click="remove" disabled>Delete</UButton>
        </div>
      </template>
    </UCard>
  </div>
</template>

<script lang="ts" setup>
import type { CarType } from '~/types/CarType.type';

const route = useRoute();
const { id } = route.params;
const { data, status, error, refresh } = await useFetch<CarType>(`/api/cartype/${id}`);
const toast = useToast();
const update = async () => {
  await $fetch(`/api/cartype/update`, {
    method: 'PUT',
    body: data.value
  });
  refresh();
  toast.add({ title: 'Updated'});
}

const remove = async () => {
  await $fetch(`/api/cartype/delete/${id}`, {
    method: 'DELETE',
  });
  await navigateTo('/settings');
}

</script>

<style lang="scss" scoped>

</style>