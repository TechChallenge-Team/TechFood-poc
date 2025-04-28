export type ItemDetailCardProps = {
  id: string;
  title: string;
  price: number;
  size: string;
  img: string;
  garnishes: any[];
  onClose: () => void;
  onAdd: (item: any) => void;
};
