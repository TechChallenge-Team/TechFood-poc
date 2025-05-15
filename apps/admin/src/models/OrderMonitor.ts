export interface OrderMonitor {
  id: string;
  number: number;
  status: OrderStatusType;
  products: OrderProduct[];
}

// export type OrderStatusType = "Received" | "InPreparation" | "Done" | "Finished"

export enum OrderStatusType {
  Received = "Criado",
  InPreparation = "Em Preparação",
  Done = "Pronto",
  Finished = "Finalizado",
}

export interface OrderProduct {
  id: string;
  name: string;
  quantity: number;
}
