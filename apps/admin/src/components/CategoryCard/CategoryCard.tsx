import { Flex, Text } from "@radix-ui/themes";
import { CategoryCardProps } from "./CategoryCard.types";

import classNames from "./CategoryCard.module.css";

export const CategoryCard = ({ name, imageUrl }: CategoryCardProps) => {
  return (
    <Flex
      className={classNames.root}
      direction="column"
      gap="2"
      align="center"
      justify="center"
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
