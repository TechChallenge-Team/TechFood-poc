import { Flex, Text } from "@radix-ui/themes";
import { CategoryCardProps } from "./CategoryCard.types";
import classNames from "./CategoryCard.module.css";
import clsx from "clsx";

export const CategoryCard = ({
  name,
  id,
  imageUrl,
  handleFilterByCategory,
  selected = false,
}: CategoryCardProps) => {
  return (
    <Flex
      className={clsx(classNames.root, selected && classNames.selected)}
      direction="column"
      gap="2"
      align="center"
      justify="center"
      onClick={() => {
        handleFilterByCategory(id);
      }}
    >
      <img src={imageUrl} alt={name} />
      <Text
        as="p"
        size="2"
        weight="medium"
        color="gray"
        className={classNames.name}
      >
        {name}
      </Text>
    </Flex>
  );
};
