<template>
    <UCard class="w-full" :key="car.id" :ui="{
            body: {
            padding: ''
            }
        }">
            <div class="grid grid-cols-4 gap-4">
                <img :src="car.imageUrl" alt="car" class="h-48 w-48 object-contain col-span-1 overflow-hidden" >
                <div class="col-span-3 p-6 relative">
                    <UBadge class="absolute right-2 top-2">{{ car.carType.name ?? 'unknown' }}</UBadge>
                    <div class="mb-3">
                        <h1 class="text-3xl">{{ car.make }} {{ car.model }}</h1>
                        <p class="text-sm"><span class="text-xs opacity-60">RegNo:</span> {{ car.licensePlate }}</p>
                    </div>
                    <UDivider />
                    <div class="grid grid-cols-4 gap-4 my-3">
                        <div>
                            <p class="text-xs opacity-60">Location</p>
                            <p class="text-sm">Main office</p>
                        </div>
                        <div>
                            <p class="text-xs opacity-60">Milage</p>
                            <p class="text-sm">{{ car.mileage }} km</p>
                        </div>
                        <div>
                            <p class="text-xs opacity-60">Fuel</p>
                            <p class="text-sm">{{ car.fuelType.name ?? 'Diesel' }}</p>
                        </div>
                        <div>
                            <p class="text-xs opacity-60">Price</p>
                            <p class="text-sm">{{ car.price }} SEK/day</p>
                        </div>
                    </div>
                </div>
            </div>
            <template #footer>
                <div class="flex gap-2">
                    <UButton color="primary" @click="$emit('rent')">Rent</UButton>
                    <UButton color="gray">Return</UButton>
                    <UButton color="black">Mark as unavailable</UButton>
                </div>
            </template>
        </UCard>
</template>

<script lang="ts" setup>
import type { Car } from '~/Types/Car.type';

interface props {
  car: Car;
}
defineProps<props>();
defineEmits(['rent', 'return', 'markUnavailable']);
</script>

<style lang="scss" scoped>

</style>