export type TOrderStatus = "INPREPARATION" | "DONE";

export type TOrder = {
  number: number;
  status: TOrderStatus;
};
