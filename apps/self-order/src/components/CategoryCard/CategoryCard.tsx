import { Box, Flex, Text } from "@radix-ui/themes";
import { CategoryCardProps } from "./CategoryCard.types";

import classNames from "./CategoryCard.module.css";
import { clsx } from "clsx";

const assetsPath = "../../assets/categories/";

export const CategoryCard = ({
  name,
  img,
  selected,
  onClick,
}: CategoryCardProps) => {
  const src = new URL(`${assetsPath}${img}`, import.meta.url).href;

  return (
    <Flex
      className={clsx(classNames.root, selected && classNames.selected)}
      direction="column"
      align="center"
      justify="center"
      onClick={onClick}
    >
      <Box className={classNames.imageContainer}>
        <img src={src} alt={name} />
      </Box>
      <Text as="p" size="1" weight="medium" color="gray">
        {name}
      </Text>
    </Flex>
  );
};
