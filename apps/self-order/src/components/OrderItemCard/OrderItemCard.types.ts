import { OrderItem, Product } from "../../models";

export type OrderItemCardProps = {
  item: OrderItem;
  product: Product;
  onRemoveClick: (item: OrderItem) => void;
};
