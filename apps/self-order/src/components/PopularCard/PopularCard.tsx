import { Flex, Text, Strong, Heading } from "@radix-ui/themes";
import { StarFilledIcon } from "@radix-ui/react-icons";
import { getReviewsCount } from "../../utilities";
import { PopularCardProps } from "./PopularCard.types";

import classNames from "./PopularCard.module.css";

export const PopularCard = ({
  title,
  price,
  rating,
  reviewsCount,
  description,
  img,
}: PopularCardProps) => {
  const src = new URL(`../../assets/products/${img}.png`, import.meta.url).href;
  return (
    <Flex className={classNames.root} direction="column" gap="2">
      <Flex direction="row">
        <img src={src} alt={title} />
        <Flex className={classNames.info} direction="column" gap="1">
          <Heading
            className={classNames.title}
            size="3"
            weight="bold"
            color="gray"
          >
            {title}
          </Heading>
          <Strong className={classNames.price}>
            <Strong>R$</Strong>
            {price}
          </Strong>
          <Flex direction="row" gap="2" align="center">
            <StarFilledIcon
              className={classNames.star}
              color="var(--amber-10)"
            />
            <Text size="1" weight="medium" color="gray">
              {rating}
            </Text>
            <Text size="1" weight="medium" color="gray">
              &bull;
            </Text>
            <Text size="1" weight="medium" color="gray">
              {getReviewsCount(reviewsCount)}
            </Text>
          </Flex>
        </Flex>
      </Flex>
      <Text
        className={classNames.description}
        size="1"
        weight="medium"
        color="gray"
        wrap="wrap"
      >
        {description}
      </Text>
    </Flex>
  );
};
