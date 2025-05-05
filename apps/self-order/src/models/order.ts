import { OrderItem } from "./order-item";

export interface Order {
  id: string;
  number: number;
  amount: number;
  discount: number;
  items: OrderItem[];
}
