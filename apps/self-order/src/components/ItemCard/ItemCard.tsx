import { Flex, Text, Strong, Heading } from "@radix-ui/themes";
import { ItemCardProps } from "./ItemCard.types";

import classNames from "./ItemCard.module.css";

const assetsPath = "../../assets/products/";

export const ItemCard = ({
  title,
  price,
  size,
  img,
  onClick,
}: ItemCardProps) => {
  const src = new URL(`${assetsPath}${img}`, import.meta.url).href;
  return (
    <Flex
      className={classNames.root}
      direction="column"
      gap="1"
      align="center"
      onClick={onClick}
    >
      <img src={src} alt={title} />
      <Heading className={classNames.title} size="3" weight="bold" color="gray">
        {title}
      </Heading>
      <Text size="1" weight="medium" color="gray" wrap="wrap">
        {size}
      </Text>
      <Strong className={classNames.price}>
        <Strong>R$ {price}</Strong>
      </Strong>
      <Text
        className={classNames.description}
        size="1"
        weight="medium"
        color="gray"
        wrap="wrap"
      ></Text>
    </Flex>
  );
};
