import type { Car } from "~/types/Car.type"

export const useCarsStore = defineStore('cars', {
  state: () => ({
    cars: [] as Car[],
  }),
  getters: {
    getAvailableCars: (state) => state.cars.filter((car) => !car.bookingId),
    getRentedCars: (state) => state.cars.filter((car) => car.bookingId),
  },
})