import { Button, Card, Flex, Grid, IconButton, Text } from "@radix-ui/themes";
import { MinusIcon, PlusIcon, XIcon } from "lucide-react";
import { ItemDetailCardProps } from "./ItemDetailCard.types";

import classNames from "./ItemDetailCard.module.css";
import { useState } from "react";
import { clsx } from "clsx";

const assetsPath = "../../assets/products/";

const GarnisheItem = ({ title, subtitle, img }: any) => {
  const [count, setCount] = useState(1);

  const src = new URL(`${assetsPath}${img}`, import.meta.url).href;

  const handleCountChange = (newCount: number) => {
    if (newCount < 0) return;
    setCount(newCount);
  };

  return (
    <Grid
      className={classNames.garnisheItem}
      columns="auto 1fr auto"
      gap="2"
      align="center"
    >
      <img src={src} />
      <Flex direction="column" gap="1" justify="center">
        <Text size="2">{title}</Text>
        <Text size="1" color="gray">
          {subtitle}
        </Text>
      </Flex>
      <Flex direction="row" gap="4" align="center">
        <IconButton
          variant="soft"
          color="gray"
          size="1"
          onClick={() => handleCountChange(count - 1)}
          disabled={count <= 1}
        >
          <MinusIcon />
        </IconButton>
        <Text size="1" color="gray">
          {count}
        </Text>
        <IconButton size="1" onClick={() => handleCountChange(count + 1)}>
          <PlusIcon />
        </IconButton>
      </Flex>
    </Grid>
  );
};

const GarnisheList = ({ items }: any) => {
  return (
    <Flex className={classNames.garnisheList} direction="column" gap="4">
      <Flex className={classNames.garnisheListItems} direction="column" gap="2">
        {items.map((item: any) => (
          <GarnisheItem key={item.id} {...item} />
        ))}
      </Flex>
      <Flex
        className={classNames.garnisheListButtons}
        direction="row"
        justify="center"
      >
        <Button size="2">Apply</Button>
      </Flex>
    </Flex>
  );
};

export const ItemDetailCard = ({
  title,
  price,
  size,
  img,
  garnishes,
  onClose,
}: ItemDetailCardProps) => {
  const [count, setCount] = useState(1);
  const [isChosingGarnishe, setIsChoosingGarnishe] = useState(false);

  const src = new URL(`${assetsPath}${img}`, import.meta.url).href;

  const handleCountChange = (newCount: number) => {
    if (newCount < 0) return;
    setCount(newCount);
  };

  return (
    <Flex className={classNames.root} direction="column">
      <Card
        className={clsx(
          classNames.card,
          isChosingGarnishe && classNames.chosingGarnishe
        )}
      >
        <Flex direction="column" align="center">
          <IconButton
            variant="outline"
            size="2"
            aria-label="Close"
            onClick={onClose}
          >
            <XIcon />
          </IconButton>
        </Flex>
        <Flex
          className={classNames.content}
          direction="column"
          align="center"
          gap="5"
        >
          <Flex
            className={classNames.itemInfo}
            direction="column"
            gap="4"
            align="center"
          >
            <img src={src} alt={title} className={classNames.img} />
            <Flex direction="column" align="center">
              <Text size="2" weight="bold">
                {title}
              </Text>
              <Text size="1" color="gray">
                {size}
              </Text>
              <Text size="2" weight="bold" className={classNames.price}>
                R$ {price}
              </Text>
            </Flex>
          </Flex>

          {isChosingGarnishe ? (
            <GarnisheList items={garnishes} />
          ) : (
            <Flex direction="column" gap="5" align="center">
              <Flex direction="row" gap="4" align="center">
                <IconButton
                  variant="soft"
                  color="gray"
                  size="1"
                  onClick={() => handleCountChange(count - 1)}
                  disabled={count <= 1}
                >
                  <MinusIcon />
                </IconButton>
                <Text size="1" color="gray">
                  {count}
                </Text>
                <IconButton
                  size="1"
                  onClick={() => handleCountChange(count + 1)}
                >
                  <PlusIcon />
                </IconButton>
              </Flex>
              <Flex direction="row" gap="4" justify="center">
                <Button
                  variant="soft"
                  color="gray"
                  size="2"
                  onClick={() => setIsChoosingGarnishe(!isChosingGarnishe)}
                  disabled={garnishes.length === 0}
                >
                  Customize
                </Button>
                <Button size="2">Done</Button>
              </Flex>
            </Flex>
          )}
        </Flex>
      </Card>
    </Flex>
  );
};
