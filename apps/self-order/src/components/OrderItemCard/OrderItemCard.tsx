import { useState } from "react";
import { Flex, Text, Heading, IconButton } from "@radix-ui/themes";
import { MinusIcon, PlusIcon, Trash2Icon } from "lucide-react";
import { t } from "../../i18n";
import { OrderItemCardProps } from "./OrderItemCard.types";

import classNames from "./OrderItemCard.module.css";

export const OrderItemCard = ({
  item,
  product: { name, img, price },
  onRemoveClick,
}: OrderItemCardProps) => {
  const [count, setCount] = useState(1);

  const src = new URL(`../../assets/products/${img}.png`, import.meta.url).href;
  const isDeleteVisible = count <= 1;

  const handleCountChange = (newCount: number) => {
    if (newCount < 1) onRemoveClick(item);
    setCount(newCount);
  };

  return (
    <Flex className={classNames.root} direction="column" gap="1" align="center">
      <img src={src} alt={name} />
      <Heading size="1" weight="bold">
        {name}
      </Heading>
      <Text size="1" color="gray">
        {t("labels.currency")}
        {price}
      </Text>
      <Flex direction="column" align="center">
        <Flex direction="row" gap="3" align="center">
          <IconButton
            variant="soft"
            color={isDeleteVisible ? "red" : "gray"}
            size="1"
            onClick={() => handleCountChange(count - 1)}
          >
            {isDeleteVisible ? <Trash2Icon /> : <MinusIcon />}
          </IconButton>
          <Text size="1" color="gray">
            {count}
          </Text>
          <IconButton size="1" onClick={() => handleCountChange(count + 1)}>
            <PlusIcon />
          </IconButton>
        </Flex>
      </Flex>
    </Flex>
  );
};
