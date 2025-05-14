export type TOrderStatus = "INPREPARATION" | "DONE";

export interface Order {
  number: number;
  status: TOrderStatus;
}
