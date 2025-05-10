export type CategoryCardProps = {
  id: string;
  name: string;
  imageUrl: string;
  selected?: boolean;
  handleFilterByCategory: (category: string) => void;
};
