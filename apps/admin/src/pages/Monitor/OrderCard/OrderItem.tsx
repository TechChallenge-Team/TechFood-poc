import { Flex, Heading, IconButton, Text } from "@radix-ui/themes";
import classNames from "./OrderItem.module.css";
import { ArrowRightIcon } from "lucide-react";
import { OrderMonitor } from "../../../models/OrderMonitor";
import clsx from "clsx";

type IOrderMonitorItemProps = {
  orderMonitor: OrderMonitor;
  selected?: boolean;
  onClick?: () => void;
};

const InfoButton = () => {
  return (
    <Flex>
      <IconButton>
        <ArrowRightIcon size="15"></ArrowRightIcon>
      </IconButton>
    </Flex>
  );
};

export const OrderItem = ({
  orderMonitor,
  selected,
  onClick,
}: IOrderMonitorItemProps) => {
  return (
    <Flex
      className={clsx(classNames.orderItem, selected && classNames.selected)}
      justify="between"
      align="center"
      onClick={onClick}
    >
      <Flex direction="column" gap="1">
        <Flex direction="column" gap="3">
          <Flex
            className={classNames.actions}
            direction="row"
            justify="between"
            gap="4"
            align="center"
          >
            <Heading
              className={classNames.title}
              size="5"
              weight="bold"
              color="gray"
            >
              Order #{orderMonitor.number}
            </Heading>
          </Flex>

          <Flex direction="row" gap="2" align="center"></Flex>
        </Flex>
      </Flex>

      <InfoButton />
    </Flex>
  );
};
