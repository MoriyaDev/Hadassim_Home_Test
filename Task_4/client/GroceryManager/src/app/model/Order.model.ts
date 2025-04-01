import { Supplier } from './supplier.model';
import { OrderItem } from './OrderItem.model';

export enum OrderStatus {
  Pending = 0,
  InProcess = 1,
  Completed = 2
}

export interface Order {
  id: number;
  supplierId: number;
  supplier: Supplier;
  createdAt: string; // אפשר גם Date אם מעדיפים
  items: OrderItem[];
  status: OrderStatus;
}
