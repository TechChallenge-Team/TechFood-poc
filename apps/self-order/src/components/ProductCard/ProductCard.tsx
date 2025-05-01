import { Flex, Text, Strong, Heading } from "@radix-ui/themes";
import { t } from "../../i18n";
import { ProductCardProps } from "./ProductCard.types";

import classNames from "./ProductCard.module.css";

export const ProductCard = ({
  item: { name, img, unit, price },
  onClick,
}: ProductCardProps) => {
  const src = new URL(`../../assets/products/${img}.png`, import.meta.url).href;
  return (
    <Flex
      className={classNames.root}
      direction="column"
      gap="1"
      align="center"
      onClick={onClick}
    >
      <img src={src} alt={name} />
      <Heading className={classNames.title} size="3" weight="bold" color="gray">
        {name}
      </Heading>
      <Text size="1" weight="medium" color="gray" wrap="wrap">
        {unit}
      </Text>
      <Strong className={classNames.price}>
        <Strong>
          {t("labels.currency")} {price}
        </Strong>
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
