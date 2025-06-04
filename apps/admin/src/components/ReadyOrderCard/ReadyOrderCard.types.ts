import { ReadyOrder } from "../../models";

export type ReadyOrderCardProps = {
  item: ReadyOrder;
  onDeliver: (item: ReadyOrder) => void;
};
