export interface OrderMonitor {
  orderId: string;
  number: number;
  status: OrderStatus;
  products: OrderProduct[];
}

export interface OrderProduct {
  imageUrl: string;
  name: string;
  quantity: number;
}

export type OrderStatus =
  | "PAID"
  | "INPREPARATION"
  | "DONE"
  | "FINISH"
  | "REJECT";
