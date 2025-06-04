import { OrderItem } from "./order-item";

export type Order = {
  id: string;
  status: "RECEIVED" | "INPREPARATION" | "DELIVERED";
  createdAt: Date;
  startedAt?: Date;
  readyAt?: Date;
  number: string;
  amount: string;
  items: OrderItem[];
};
