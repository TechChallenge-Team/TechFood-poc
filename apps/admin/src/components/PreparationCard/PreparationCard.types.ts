import { Preparation } from "../../models";

export type PreparationCardProps = {
  item: Preparation;
  onStart?: (item: Preparation) => void;
  onReady?: (item: Preparation) => void;
};
