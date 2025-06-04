import { PreparationItem } from "./preparation-item";

export type Preparation = {
  id: string;
  status: "PENDING" | "STARTED" | "READY";
  createdAt: Date;
  startedAt?: Date;
  readyAt?: Date;
  number: string;
  items: PreparationItem[];
};
