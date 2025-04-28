import { Flex, Text, Heading, IconButton } from "@radix-ui/themes";
import { Trash2Icon } from "lucide-react";
import { OrderItemCardProps } from "./OrderItemCard.types";

import classNames from "./OrderItemCard.module.css";

const assetsPath = "../../assets/products/";

export const OrderItemCard = ({
  id,
  title,
  price,
  img,
  onRemoveClick,
}: OrderItemCardProps) => {
  const src = new URL(`${assetsPath}${img}`, import.meta.url).href;

  return (
    <Flex className={classNames.root} direction="row" gap="2" align="center">
      <img src={src} alt={title} />
      <Flex direction="column" flexGrow="1">
        <Heading size="1" weight="bold">
          {title}
        </Heading>
        <Text size="1" color="gray">
          R$ {price}
        </Text>
      </Flex>
      <IconButton
        size="1"
        onClick={() => onRemoveClick(id)}
        aria-label="Remove item"
      >
        <Trash2Icon />
      </IconButton>
    </Flex>
  );
};
