import type { CarType } from "./CarType.type";
import type { FuelType } from "./FuelType.type";

export type Car = {
    id: number;
    carType: CarType; 
    fuelType: FuelType;
    imageUrl: string;
    mileage: number;
    seats: number;
    licensePlate: string;
    make: string;
    model: string;
    year: number;
    color: string;
    isAvailable: boolean;
    price: number;
};