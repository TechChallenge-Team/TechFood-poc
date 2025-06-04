import { Flex, Heading } from "@radix-ui/themes";
import { PreparationsCardProps } from "./PreparationsCard.types";

import classNames from "./PreparationsCard.module.css";

export const PreparationsCard = ({
  items,
  title,
  type,
}: PreparationsCardProps) => {
  return (
    <Flex className={classNames.container} direction="column">
      <Flex className={classNames[type]}>
        <Heading as="h2" size="8">
          {title}
        </Heading>
      </Flex>
      <Flex className={classNames.item} wrap="wrap" gap="6">
        {items.map((item) => (
          <Flex key={item.id}>
            <Heading as="h3" size="8">
              {item.number}
            </Heading>
          </Flex>
        ))}
      </Flex>
    </Flex>
  );
};
