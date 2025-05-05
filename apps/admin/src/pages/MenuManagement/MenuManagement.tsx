import { Box, Flex, Heading, TextField } from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { CategoryCard, PopularCard } from "../../components";
import { ProductModal } from "../../components/ProductModal";
import { Category } from "../../models/Category";

import classNames from "./MenuManagement.module.css";
import { useEffect, useState } from "react";

const categories = [
  { name: "Hambugers", img: "hamburger" },
  { name: "Bebidas", img: "soda" },
  { name: "Acompanhamentos", img: "fried-chicken" },
  { name: "Sobremesas", img: "donut" },
];

const popular = [
  {
    title: "Hambuger",
    category: "Hambugers",
    price: "10,00",
    rating: 4.5,
    reviewsCount: 100,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "1",
  },
  {
    title: "Hambuger 2",
    category: "Hambugers",
    price: "10,00",
    rating: 4.5,
    reviewsCount: 1001,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "2",
  },
  {
    title: "Coca-Cola",
    category: "Bebidas",
    price: "5,00",
    rating: 4.0,
    reviewsCount: 51,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "3",
  },
  {
    title: "Banana split",
    category: "Sobremesas",
    price: "15,00",
    rating: 5.0,
    reviewsCount: 151,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "4",
  },
];

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
  const [categories, setCategories] = useState<Category[]>([]);

  useEffect(() => {}, []);

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
        {popular.map((popu, i) => (
          <PopularCard key={i} {...popu} />
        ))}
      </Section>
    </Flex>
  );
};
