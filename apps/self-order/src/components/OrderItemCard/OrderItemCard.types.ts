export type OrderItemCardProps = {
  id: string;
  title: string;
  price: string;
  img: string;
  count?: number;
  onRemoveClick: (item: any) => void;
};
