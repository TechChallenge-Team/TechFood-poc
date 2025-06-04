export type PreparationStatus = "PENDING" | "STARTED" | "READY";

export interface Preparation {
  id: string;
  number: number;
  status: PreparationStatus;
}
