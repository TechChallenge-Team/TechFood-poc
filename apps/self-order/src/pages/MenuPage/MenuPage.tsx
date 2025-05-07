import { useEffect, useState } from "react";
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
import axios from "axios";

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
  selectedItem: Category | undefined;
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
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<
    Category | undefined
  >();
  const [orderItems, setOrderItems] = useState<OrderItem[]>([]);

  const selectedItems = products.filter(
    (product) => product.categoryId === selectedCategory?.id
  );

  function handleRemoveOrderItem(item: OrderItem) {
    setOrderItems((prevItems) => prevItems.filter((i) => i !== item));
  }

  useEffect(() => {
    const loadCategories = async () => {
      const result = await axios.get<Category[]>("/api/v1/Categories");
      setCategories(result.data);
      setSelectedCategory(result.data[0]);
    };

    const loadProducts = async () => {
      const result = await axios.get<Product[]>("/api/v1/Products");
      setProducts(result.data);
    };

    loadCategories();
    loadProducts();
  }, []);

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
              product={products.find((i) => i.id === item.productId) as Product}
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
