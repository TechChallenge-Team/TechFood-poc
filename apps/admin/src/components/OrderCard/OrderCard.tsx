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
          <Flex gap="1">
            <Strong>{orderMonitor.status}</Strong>
          </Flex>
          <Flex direction="row" gap="2" align="center">
            <ul className={classNames.description}>
              {orderMonitor.products.map((item) => (
                <li key={item.id} className={classNames.listItem}>
                  {item.name}
                </li>
              ))}
            </ul>
          </Flex>
        </Flex>
      </Flex>
    </Flex>
  );
};
