import { Flex, Text, Strong, Heading } from "@radix-ui/themes";
import { ProductCardProps } from "./ProductCard.types";
import { Pencil1Icon, TrashIcon } from "@radix-ui/react-icons";
import classNames from "./ProductCard.module.css";

export const ProductCard = ({
  price,
  description,
  imageUrl,
  name,
  id,
  handleDeleteProduct,
}: ProductCardProps) => {
  return (
    <Flex className={classNames.root} direction="column" gap="2">
      <Flex
        className={classNames.actions}
        direction="row"
        justify="between"
        gap="4"
        align="center"
      >
        <Heading
          className={classNames.title}
          size="4"
          weight="bold"
          color="gray"
        >
          {name}
        </Heading>
        <Flex gap={"3"}>
          <Flex className={classNames.edit} align={"center"} justify={"center"}>
            <Pencil1Icon
              className={classNames.editIcon}
              width={16}
              height={16}
            />
          </Flex>
          <Flex
            className={classNames.delete}
            align={"center"}
            justify={"center"}
            onClick={() => {
              handleDeleteProduct(id);
            }}
          >
            <TrashIcon
              className={classNames.deleteIcon}
              width={16}
              height={16}
            />
          </Flex>
        </Flex>
      </Flex>
      <Flex direction="row" align={"center"} gap="2" justify="start">
        <Flex className={classNames.imageContainer}>
          <img src={imageUrl} alt={name} />
        </Flex>
        <Flex className={classNames.info} direction="column" gap="1">
          <Flex gap="1" className={classNames.price}>
            <Strong>R$</Strong>
            {Number(price).toFixed(2)}
          </Flex>
          <Flex direction="row" gap="2" align="center">
            <Text size="2" weight="medium" color="gray">
              {description}
            </Text>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  );
};
