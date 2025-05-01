import { useState } from "react";
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
import { t } from "../../i18n";
import { Category, OrderItem, Product } from "../../models";
import {
  CategoryCard,
  ProductCard,
  OrderItemBuilderCard,
  OrderItemCard,
} from "../../components";

import classNames from "./MenuPage.module.css";

const categories: Category[] = [
  { id: "1", name: "Hambugers", img: "hamburger" },
  { id: "2", name: "Bebidas", img: "soda" },
  { id: "3", name: "Acompanhamentos", img: "fried-chicken" },
  { id: "4", name: "Sobremesas", img: "donut" },
];

const items: Product[] = [
  {
    id: "1",
    name: "Hambuger",
    description: "Hambuger",
    categoryId: "1",
    price: 10.0,
    unit: "180g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "2",
    name: "Hambuger 2",
    description: "Hambuger 2",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "3",
    name: "Hambuger 3",
    description: "Hambuger 3",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "4",
    name: "Hambuger 4",
    description: "Hambuger 4",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "5",
    name: "Hambuger 5",
    description: "Hambuger 5",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "6",
    name: "Hambuger 6",
    description: "Hambuger 6",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "7",
    name: "Hambuger 7",
    description: "Hambuger 7",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "8",
    name: "Hambuger 8",
    description: "Hambuger 8",
    categoryId: "1",
    price: 12.0,
    unit: "280g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "9",
    name: "Hambuger 9",
    description: "Hambuger 9",
    categoryId: "1",
    price: 25.0,
    unit: "580g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "10",
    name: "Hambuger 10",
    description: "Hambuger 10",
    categoryId: "1",
    price: 30.0,
    unit: "680g",
    img: "2",
    outOfStock: false,
    garnishes: [
      {
        id: "1",
        name: "Option 1",
        description: "Option 1",
        img: "1",
      },
      {
        id: "2",
        name: "Option 2",
        description: "Option 2",
        img: "1",
      },
      {
        id: "3",
        name: "Option 3",
        description: "Option 3",
        img: "1",
      },
      {
        id: "4",
        name: "Option 4",
        description: "Option 4",
        img: "1",
      },
      {
        id: "5",
        name: "Option 5",
        description: "Option 5",
        img: "1",
      },
      {
        id: "6",
        name: "Option 6",
        description: "Option 6",
        img: "1",
      },
      {
        id: "7",
        name: "Option 7",
        description: "Option 7",
        img: "1",
      },
      {
        id: "8",
        name: "Option 8",
        description: "Option 8",
        img: "1",
      },
    ],
  },
  {
    id: "11",
    name: "Coca-Cola",
    description: "Coca-Cola",
    categoryId: "2",
    price: 5.0,
    img: "3",
    outOfStock: false,
    garnishes: [],
  },
  {
    id: "12",
    name: "Coca-Cola 2",
    description: "Coca-Cola 2",
    categoryId: "2",
    price: 5.0,
    img: "3",
    outOfStock: false,
    garnishes: [],
  },
];

const sortByOptions = [
  { value: "popular", label: () => t("menuPage.sort.popular") },
  { value: "name", label: () => t("menuPage.sort.name") },
  { value: "price", label: () => t("menuPage.sort.price") },
];

const CategoriesCard = ({
  items,
  selectedItem,
  onSelectedItem,
}: {
  items: Category[];
  selectedItem: Category;
  onSelectedItem: (item: Category) => void;
}) => {
  return (
    <Flex
      className={classNames.categoriesCard}
      direction="row"
      gap="4"
      overflowX="auto"
    >
      {items.map((item: Category) => (
        <CategoryCard
          key={item.id}
          item={item}
          selected={item == selectedItem}
          onClick={() => {
            onSelectedItem(item);
          }}
        />
      ))}
    </Flex>
  );
};

const ItemsCard = ({
  items,
  onSelectedItem,
}: {
  items: Product[];
  onSelectedItem: (item: Product) => void;
}) => {
  return (
    <Flex
      className={classNames.itemsCard}
      direction="column"
      gap="4"
      overflow="hidden"
    >
      <Flex direction="row" justify="between">
        <Heading size="5" as="h1" weight="regular">
          <Strong>{t("menuPage.choose")}</Strong> {t("menuPage.order")}
        </Heading>
        <Flex align="center" gap="1">
          <Text size="1">{t("menuPage.sortBy")}</Text>
          <Select.Root defaultValue="popular" size="1">
            <Select.Trigger className={classNames.sort} variant="ghost" />
            <Select.Content>
              {sortByOptions.map((option) => (
                <Select.Item key={option.value} value={option.value}>
                  {option.label()}
                </Select.Item>
              ))}
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
        {items.map((item, i) => (
          <ProductCard
            key={i}
            item={item}
            onClick={() => onSelectedItem(item)}
          />
        ))}
      </Flex>
    </Flex>
  );
};

export const MenuPage = () => {
  const [selectedItem, setSelectedItem] = useState<Product | null>(null);
  const [selectedCategory, setSelectedCategory] = useState(categories[0]);
  const [orderItems, setOrderItems] = useState<OrderItem[]>([]);

  const selectedItems = items.filter(
    (item) => item.categoryId === selectedCategory.id
  );

  function handleRemoveOrderItem(item: OrderItem) {
    setOrderItems((prevItems) => prevItems.filter((i) => i !== item));
  }

  return (
    <Flex className={classNames.root} direction="row">
      <Flex className={classNames.left} direction="column" gap="4" flexGrow="1">
        <Flex direction="row" justify="between">
          <Heading size="5" as="h1" weight="regular">
            <Strong>{t("menuPage.menu")}</Strong> {t("menuPage.category")}
          </Heading>
          <Box width="100%" maxWidth="500px">
            <TextField.Root
              className={classNames.search}
              placeholder={t("menuPage.search")}
              size="2"
            >
              <TextField.Slot>
                <MagnifyingGlassIcon height="25" width="25" />
              </TextField.Slot>
            </TextField.Root>
          </Box>
        </Flex>
        <CategoriesCard
          items={categories}
          selectedItem={selectedCategory}
          onSelectedItem={setSelectedCategory}
        />
        <ItemsCard items={selectedItems} onSelectedItem={setSelectedItem} />
        {selectedItem && (
          <OrderItemBuilderCard
            item={selectedItem}
            onClose={() => setSelectedItem(null)}
            onAdd={(item: OrderItem) => {
              setOrderItems((prevItems) => [...prevItems, item]);
              setSelectedItem(null);
            }}
          />
        )}
      </Flex>
      <Flex className={classNames.right} direction="column">
        <Heading className={classNames.header}>{t("menuPage.myOrder")}</Heading>
        <Flex direction="column" overflowY="auto" flexGrow="1">
          {orderItems.map((item, i) => (
            <OrderItemCard
              key={i}
              item={item}
              product={items.find((i) => i.id === item.productId) as Product}
              onRemoveClick={handleRemoveOrderItem}
            />
          ))}
        </Flex>
        <Flex direction="column" gap="4" align="center">
          <Heading as="h5">{t("labels.total")}</Heading>
          <Text>
            {t("labels.currency")}
            {orderItems
              .reduce((total, item) => total + item.unitPrice, 0)
              .toFixed(2)}
          </Text>
          <Button
            size="4"
            className={classNames.doneButton}
            disabled={!orderItems.length}
          >
            {t("labels.done")}
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};
