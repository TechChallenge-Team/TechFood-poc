import { Preparation } from "../../models";

export type PreparationsCardProps = {
  items: Preparation[];
  title: string;
  type: "preparing" | "done";
};
