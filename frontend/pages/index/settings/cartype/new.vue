<template>
  <div>
    <UForm :state="state" @submit="submit">
      <UFormGroup label="Name">
        <UInput v-model="state.name" />
      </UFormGroup>
      <UFormGroup label="Multiplier">
        <UInput v-model="state.multiplier" type="number" :min="1" step="any" />
      </UFormGroup>
      <UFormGroup label="Multiply days">
        <UCheckbox v-model="state.multiplyDays" />
      </UFormGroup>
      <UFormGroup label="Multiply mileage">
        <UCheckbox v-model="state.multiplyMileage" />
      </UFormGroup>
      <UFormGroup label="Pay for mileage">
        <UCheckbox v-model="state.payForMileage"></UCheckbox>
      </UFormGroup>
      <UButton type="submit" color="primary">Create</UButton>
    </UForm>
  </div>
</template>

<script lang="ts" setup>
  const state = reactive({
    name: 'New Car Type',
    multiplier: 1,
    multiplyDays: false,
    multiplyMileage: false,
    payForMileage: false
  })

  const submit = async () => {
    await $fetch('/api/cartype/create', {
      method: 'POST',
      body: state
    })
    await navigateTo('/settings')
  }
</script>

<style lang="scss" scoped>

</style>