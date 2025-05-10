export type ProductCardProps = {
  id: string;
  name: string;
  price: string;
  description: string;
  imageUrl: string;
  categoryId: string;
  handleDeleteProduct: (id: string) => void;
};
