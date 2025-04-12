import { Button, Flex, Heading, TextField } from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { CategoryCard, PopularCard } from "../../components";

import classNames from "./MenuManagement.module.css";

const categories = [
  { name: "Hambugers", img: "hamburger.png" },
  { name: "Bebidas", img: "soda.png" },
  { name: "Acompanhamentos", img: "fried-chicken.png" },
  { name: "Sobremesas", img: "donut.png" },
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
    img: "1.png",
  },
  {
    title: "Hambuger 2",
    category: "Hambugers",
    price: "10,00",
    rating: 4.5,
    reviewsCount: 1001,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "2.png",
  },
  {
    title: "Coca-Cola",
    category: "Bebidas",
    price: "5,00",
    rating: 4.0,
    reviewsCount: 51,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "3.png",
  },
  {
    title: "Banana split",
    category: "Sobremesas",
    price: "15,00",
    rating: 5.0,
    reviewsCount: 151,
    description:
      "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
    img: "4.png",
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
  return (
    <Flex direction="column">
      <Flex direction="row" justify="between">
        <TextField.Root
          className={classNames.search}
          placeholder="Search"
          size="3"
        >
          <TextField.Slot>
            <MagnifyingGlassIcon height="25" width="25" />
          </TextField.Slot>
        </TextField.Root>
        <Button className={classNames.button} size="3">
          Add New Item
        </Button>
      </Flex>
      <Section title="Categories">
        {categories.map((category, i) => (
          <CategoryCard key={i} {...category} />
        ))}
      </Section>
      <Section title="Popular This Week">
        {popular.map((popu, i) => (
          <PopularCard key={i} {...popu} />
        ))}
      </Section>
    </Flex>
  );
};
