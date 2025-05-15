import { Flex, Heading, Strong } from "@radix-ui/themes";
import { OrderMonitor } from "../../models/OrderMonitor";
import classNames from "./OrderCard.module.css";

interface IOrderMonitorCardProps {
  orderMonitor: OrderMonitor;
}

export const OrderCard = ({ orderMonitor }: IOrderMonitorCardProps) => {
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
          {orderMonitor.number}
        </Heading>
      </Flex>
      <Flex direction="row" align={"center"} gap="2" justify="start">
        <Flex className={classNames.info} direction="column" gap="1">
          <Flex gap="1" className={classNames.description}>
            {orderMonitor.status}
          </Flex>
          <Flex direction="row" gap="2" align="center">
            {/* <Text
              className={classNames.description}
              size="2"
              weight="medium"
              color="gray"
            > */}
            {orderMonitor.products.map((item) => (
              <li key={item.id}>{item.name}</li>
            ))}
            {/* </Text> */}
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  );
};
