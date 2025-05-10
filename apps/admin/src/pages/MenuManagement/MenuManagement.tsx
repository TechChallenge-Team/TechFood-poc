import {
  Flex,
  Heading,
  TextField,
  AlertDialog,
  Button,
} from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { CategoryCard, ProductCard, ProductCardProps } from "../../components";
import { ProductModal } from "../../components/ProductModal";
import { Category } from "../../models/Category";
import { t } from "../../i18n";
import { normalizeText } from "../../utilities/normalizeText";

import classNames from "./MenuManagement.module.css";
import { useEffect, useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";

const Section = ({ title, direction, children }: any) => {
  return (
    <Flex className={classNames.section} direction="column" gap="4">
      <Heading size="4" as="h2">
        {title}
      </Heading>
      <Flex gap="4" wrap="wrap" direction={direction}>
        {children}
      </Flex>
    </Flex>
  );
};

export const MenuManagement = () => {
  const [products, setProducts] = useState<ProductCardProps[]>([]);
  const [productsFiltered, setProductsFiltered] = useState<ProductCardProps[]>(
    []
  );
  const [search, setSearch] = useState("");
  const [categories, setCategories] = useState<Category[]>([]);
  const [categorySelected, setCategorySelected] = useState("");

  const handleFilterByCategory = (category: string) => {
    if (category === categorySelected) {
      setProductsFiltered(products);
      setCategorySelected("");
      return;
    }

    const filtered = products.filter(
      (product) => product.categoryId === category
    );
    setProductsFiltered(filtered);
    setCategorySelected(category);
  };

  const handleAddProduct = (product: ProductCardProps) => {
    setProducts((prev) => [...prev, product]);
    setProductsFiltered((prev) => [...prev, product]);
  };

  const handleDeleteProduct = async (id: string) => {
    const result = await axios.delete(`/api/v1/Products/${id}`);
    if (result.status !== 200) {
      toast.error("Error deleting product");
      return;
    }
    setProducts((prev) => prev.filter((product) => product.id !== id));
    setProductsFiltered((prev) => prev.filter((product) => product.id !== id));
  };

  useEffect(() => {
    const filtered = products.filter((product) =>
      normalizeText(product.name).includes(normalizeText(search))
    );
    setProductsFiltered(filtered);
    setCategorySelected("");
  }, [search, products]);

  useEffect(() => {
    const fetchProductsAndCategories = async () => {
      const [productsResponse, categoriesResponse] = await Promise.all([
        axios.get<ProductCardProps[]>("/api/v1/Products"),
        axios.get<Category[]>("/api/v1/Categories"),
      ]);
      setProducts(productsResponse.data);
      setProductsFiltered(productsResponse.data);
      setCategories(categoriesResponse.data);
    };

    fetchProductsAndCategories();
  }, []);

  return (
    <Flex direction="column">
      <Flex gap="8" align="center">
        <TextField.Root
          className={classNames.search}
          placeholder="Search"
          size="3"
          onChange={(e) => {
            setSearch(e.target.value);
          }}
        >
          <TextField.Slot>
            <MagnifyingGlassIcon height="25" width="25" />
          </TextField.Slot>
        </TextField.Root>
        <ProductModal
          categories={categories}
          handleAddProducts={handleAddProduct}
        />
      </Flex>
      <Flex direction="row" justify="between"></Flex>
      {!search && (
        <Section title={t("MenuManagement.Categories")}>
          {categories.map((category) => (
            <CategoryCard
              key={category.id}
              {...category}
              selected={categorySelected === category.id}
              handleFilterByCategory={handleFilterByCategory}
            />
          ))}
        </Section>
      )}
      <Section title={t("MenuManagement.Products")}>
        {productsFiltered.map((product) => (
          <ProductCard
            key={product.id}
            {...product}
            handleDeleteProduct={handleDeleteProduct}
          />
        ))}
      </Section>

      {/* <AlertDialog.Root>
        <AlertDialog.Trigger>
          <Button color="red">Revoke access</Button>
        </AlertDialog.Trigger>
        <AlertDialog.Content maxWidth="450px">
          <AlertDialog.Title>Revoke access</AlertDialog.Title>
          <AlertDialog.Description size="2">
            Are you sure? This application will no longer be accessible and any
            existing sessions will be expired.
          </AlertDialog.Description>

          <Flex gap="3" mt="4" justify="end">
            <AlertDialog.Cancel>
              <Button variant="soft" color="gray">
                Cancel
              </Button>
            </AlertDialog.Cancel>
            <AlertDialog.Action>
              <Button variant="solid" color="red">
                Revoke access
              </Button>
            </AlertDialog.Action>
          </Flex>
        </AlertDialog.Content>
      </AlertDialog.Root> */}
    </Flex>
  );
};
