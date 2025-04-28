import {
  Box,
  Flex,
  Heading,
  Select,
  Strong,
  TextField,
  Text,
  Button,
} from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import {
  CategoryCard,
  ItemCard,
  ItemDetailCard,
  OrderItemCard,
} from "../../components";

import classNames from "./MenuPage.module.css";
import { useState } from "react";

const categories = [
  { id: 1, name: "Hambugers", img: "hamburger.png" },
  { id: 2, name: "Bebidas", img: "soda.png" },
  { id: 3, name: "Acompanhamentos", img: "fried-chicken.png" },
  { id: 4, name: "Sobremesas", img: "donut.png" },
];

const items = [
  {
    id: 1,
    title: "Hambuger",
    category: 1,
    price: "10,00",
    size: "180g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 2,
    title: "Hambuger 2",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 3,
    title: "Hambuger 3",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 4,
    title: "Hambuger 4",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 5,
    title: "Hambuger 5",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 6,
    title: "Hambuger 6",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 7,
    title: "Hambuger 7",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 8,
    title: "Hambuger 8",
    category: 1,
    price: "12,00",
    size: "280g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 9,
    title: "Hambuger 9",
    category: 1,
    price: "25,00",
    size: "580g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 10,
    title: "Hambuger 10",
    category: 1,
    price: "30,00",
    size: "680g",
    img: "2.png",
    garnishes: [
      { id: 1, title: "Option 1", subtitle: "Option 1", img: "1.png" },
      { id: 2, title: "Option 2", subtitle: "Option 2", img: "1.png" },
      { id: 3, title: "Option 3", subtitle: "Option 3", img: "1.png" },
      { id: 4, title: "Option 4", subtitle: "Option 4", img: "1.png" },
      { id: 5, title: "Option 5", subtitle: "Option 5", img: "1.png" },
      { id: 6, title: "Option 6", subtitle: "Option 6", img: "1.png" },
      { id: 7, title: "Option 7", subtitle: "Option 7", img: "1.png" },
      { id: 8, title: "Option 8", subtitle: "Option 8", img: "1.png" },
    ],
  },
  {
    id: 11,
    title: "Coca-Cola",
    category: 2,
    price: "5,00",
    img: "3.png",
    garnishes: [],
  },
  {
    id: 12,
    title: "Coca-Cola 2",
    category: 2,
    price: "5,00",
    img: "3.png",
    garnishes: [],
  },
];

const CategoriesCard = ({ categories, selectedItem, onSelectedItem }: any) => {
  return (
    <Flex
      className={classNames.categoriesCard}
      direction="row"
      gap="4"
      overflowX="auto"
    >
      {categories.map((category: any) => (
        <CategoryCard
          key={category.id}
          {...category}
          selected={category == selectedItem}
          onClick={() => {
            onSelectedItem(category);
          }}
        />
      ))}
    </Flex>
  );
};

const ItemsCard = ({ items, onSelectedItem }: any) => {
  return (
    <Flex
      className={classNames.itemsCard}
      direction="column"
      gap="4"
      overflow="hidden"
    >
      <Flex direction="row" justify="between">
        <Heading size="5" as="h1" weight="regular">
          <Strong>Choose</Strong> Order
        </Heading>
        <Flex align="center" gap="1">
          <Text size="1">Sort By</Text>
          <Select.Root defaultValue="popular" size="1">
            <Select.Trigger className={classNames.sort} variant="ghost" />
            <Select.Content>
              <Select.Item value="popular">Popular</Select.Item>
              <Select.Item value="name">Name</Select.Item>
            </Select.Content>
          </Select.Root>
        </Flex>
      </Flex>
      <Flex
        className={classNames.items}
        direction="row"
        gap="4"
        wrap="wrap"
        overflowY="auto"
      >
        {items.map((item: any, i: number) => (
          <ItemCard key={i} {...item} onClick={() => onSelectedItem(item)} />
        ))}
      </Flex>
    </Flex>
  );
};

export const MenuPage = () => {
  const [selectedItem, setSelectedItem] = useState<any>(null);
  const [selectedCategory, setSelectedCategory] = useState(categories[0]);
  const [orderItems, setOrderItems] = useState<any[]>([]);

  const selectedItems = items.filter(
    (item) => item.category === selectedCategory.id
  );

  function handleRemoveOrderItem(item: any) {
    setOrderItems((prevItems: any) =>
      prevItems.filter((i: any) => i.id !== item)
    );
  }

  return (
    <Flex className={classNames.root} direction="row">
      <Flex className={classNames.left} direction="column" gap="4" flexGrow="1">
        <Flex direction="row" justify="between">
          <Heading size="5" as="h1" weight="regular">
            <Strong>Menu</Strong> Category
          </Heading>
          <Box width="100%" maxWidth="500px">
            <TextField.Root
              className={classNames.search}
              placeholder="Search for food"
              size="2"
            >
              <TextField.Slot>
                <MagnifyingGlassIcon height="25" width="25" />
              </TextField.Slot>
            </TextField.Root>
          </Box>
        </Flex>
        <CategoriesCard
          categories={categories}
          selectedItem={selectedCategory}
          onSelectedItem={setSelectedCategory}
        />
        <ItemsCard items={selectedItems} onSelectedItem={setSelectedItem} />
        {selectedItem && (
          <ItemDetailCard
            {...selectedItem}
            onClose={() => setSelectedItem(null)}
            onAdd={(item: any) => {
              setOrderItems((prevItems) => [...prevItems, item]);
              setSelectedItem(null);
            }}
          />
        )}
      </Flex>
      <Flex className={classNames.right} direction="column" gap="4">
        <Heading>My Order</Heading>
        <Flex direction="column" flexGrow="1">
          <Flex direction="column" gap="3" overflowY="auto" flexGrow="1">
            {orderItems.map((item: any, i: number) => (
              <OrderItemCard
                key={i}
                {...item}
                onRemoveClick={handleRemoveOrderItem}
              />
            ))}
          </Flex>
          <Flex direction="column" gap="4" align="center">
            <Heading as="h5">Total</Heading>
            <Text>
              $
              {orderItems
                .reduce(
                  (total, item) =>
                    total + parseFloat(item.price.replace(",", ".")),
                  0
                )
                .toFixed(2)}
            </Text>
            <Button
              size="4"
              className={classNames.doneButton}
              disabled={!orderItems.length}
            >
              Done
            </Button>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  );
};
