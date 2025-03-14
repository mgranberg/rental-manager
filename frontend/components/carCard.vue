<template>
    <UCard class="w-full" :key="car.id" :ui="{
            body: {
            padding: ''
            }
        }">
            <div class="grid grid-cols-4 gap-4">
                <div class="h-48 w-48 col-span-1 bg-gray-200" v-if="!car.imageUrl">
                    <div class="flex justify-center items-center h-full">
                        <UIcon class="w-10 h-10" name="i-mdi-car-multiple" />
                    </div>

                </div>
                <img v-else :src="car.imageUrl" alt="car" class="h-48 w-48 object-contain col-span-1 overflow-hidden" >
                <div class="col-span-3 p-6 relative">
                    <UBadge class="absolute right-2 top-2">{{ car.carType?.name ?? 'unknown' }}</UBadge>
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
                            <p class="text-sm">{{ car.fuelType ?? 'vet ej' }}</p>
                        </div>
                    </div>
                </div>
            </div>
            <template #footer>
                <div class="flex gap-2">
                    <UButton color="primary" @click="$emit('rent')" v-if="!car.bookingId">Rent</UButton>
                    <UButton color="gray" v-if="car.bookingId" @click="$emit('return')">Return</UButton>
                </div>
            </template>
        </UCard>
</template>

<script lang="ts" setup>
import type { Car } from '~/types/Car.type';

interface props {
  car: Car;
  availableOnly?: boolean;
}
defineProps<props>();
defineEmits(['rent', 'return', 'markUnavailable']);
</script>

<style lang="scss" scoped>

</style>