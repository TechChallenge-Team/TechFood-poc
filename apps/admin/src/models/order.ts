import { Customer } from "./customer";
import { OrderItem } from "./order-item";

export interface Order {
  id: string;
  status: "RECEIVED" | "INPREPARATION" | "DELIVERED";
  createdAt: Date;
  startedAt?: Date;
  readyAt?: Date;
  number: string;
  amount: string;
  customer: Customer;
  items: OrderItem[];
}
