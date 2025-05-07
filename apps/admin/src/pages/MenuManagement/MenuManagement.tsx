import { Flex, Heading, TextField } from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { CategoryCard, PopularCard, PopularCardProps } from "../../components";
import { ProductModal } from "../../components/ProductModal";
import { Category } from "../../models/Category";

import classNames from "./MenuManagement.module.css";
import { useEffect, useState } from "react";
import axios from "axios";

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
  const [popularCards, setProducts] = useState<PopularCardProps[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  useEffect(() => {
    const loadCategories = async () => {
      const result = await axios.get<Category[]>("/api/v1/Categories");
      setCategories(result.data);
    };
    const loadProducts = async () => {
      const result = await axios.get<PopularCardProps[]>("/api/v1/Products");
      setProducts(result.data);
    };

    loadProducts();
    loadCategories();
  }, []);

  return (
    <Flex direction="column">
      <Flex direction="row" justify="between"></Flex>
      <Section title="Categories">
        {categories.map((category, i) => (
          <CategoryCard key={i} {...category} />
        ))}
      </Section>
      <Flex gap="8" align="center">
        <TextField.Root
          className={classNames.search}
          placeholder="Search"
          size="3"
        >
          <TextField.Slot>
            <MagnifyingGlassIcon height="25" width="25" />
          </TextField.Slot>
        </TextField.Root>
        <ProductModal />
      </Flex>
      <Section title="">
        {popularCards.map((popu, i) => (
          <PopularCard key={i} {...popu} />
        ))}
      </Section>
    </Flex>
  );
};
