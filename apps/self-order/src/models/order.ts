import { OrderItem } from "./order-item";

export interface Order {
  id: string;
  number: number;
  amount: number;
  totalAmount: number;
  items: OrderItem[];
}
