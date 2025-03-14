import type { CarType } from "./CarType.type";

export type Car = {
    id: number;
    carType: CarType; 
    fuelType: string;
    imageUrl: string;
    mileage: number;
    seats: number;
    licensePlate: string;
    make: string;
    model: string;
    year: number;
    color?: string;
    bookingId?: number;
};