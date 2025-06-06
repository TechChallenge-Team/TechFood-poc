import { useEffect, useState } from "react";
import {
  Button,
  Card,
  Flex,
  Heading,
  IconButton,
  SegmentedControl,
  Separator,
  Text,
} from "@radix-ui/themes";
import { ArrowRightIcon } from "lucide-react";
import { clsx } from "clsx";
import { Order, OrderItem } from "../../models";
import { t } from "../../i18n";
import api from "../../api";

import classNames from "./OrdersPage.module.css";

const INTERVAL = 5000; // 5s

const status = [
  { value: "RECEIVED", label: () => t("ordersPage.status.received") },
  { value: "INPREPARATION", label: () => t("ordersPage.status.inPreparation") },
  { value: "DELIVERED", label: () => t("ordersPage.status.delivered") },
];

const OrderCard = ({
  item,
  selected,
  onClick,
}: {
  item: Order;
  selected?: boolean;
  onClick?: () => void;
}) => {
  return (
    <Flex
      className={clsx(classNames.orderItem, selected && classNames.selected)}
      gap="5"
      onClick={onClick}
    >
      <Flex direction="column" flexGrow="1">
        <Text weight="bold">
          {t("labels.orderNumber", { number: item.number })}
        </Text>
        <Text size="1" color="gray">
          {item.createdAt.toLocaleString()}
        </Text>
      </Flex>
      <Flex className={classNames.amount} align="center" justify="center">
        <Text weight="bold">
          <Text className={classNames.symbol}>{t("labels.currency")}</Text>
          {item.amount}
        </Text>
      </Flex>
      <Flex className={classNames.control} align="center" justify="center">
        <IconButton size="1" disabled={!selected}>
          <ArrowRightIcon size="15" />
        </IconButton>
      </Flex>
    </Flex>
  );
};

const OrderItemCard = ({ item }: { item: OrderItem }) => {
  return (
    <Flex justify="between" align="center">
      <Flex direction="row" gap="4" align="center">
        <img
          src={item.imageUrl}
          alt={item.name}
          className={classNames.itemImage}
        />
        <Flex direction="column">
          <Text weight="bold">{item.name}</Text>
          <Text size="1" color="gray">
            {item.quantity}x
          </Text>
        </Flex>
      </Flex>
      <Text weight="bold">
        <Text className={classNames.symbol}>{t("labels.currency")}</Text>
        {item.price}
      </Text>
    </Flex>
  );
};

export const OrdersPage = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedStatus, setSelectedStatus] = useState<string>("RECEIVED");
  const [selectedOrderId, setSelectedOrderId] = useState<string | null>(null);

  const selectedOrder = orders.find((item) => item.id === selectedOrderId);

  useEffect(() => {
    const fetchData = async () => {
      const { data } = await api.get<Order[]>("/v1/orders");

      setOrders(data);
    };

    fetchData();

    const interval = setInterval(() => fetchData(), INTERVAL);
    return () => clearInterval(interval);
  }, []);

  return (
    <Flex className={classNames.root} gap="4">
      <Card className={classNames.cardIn} size="2">
        <Heading className={classNames.title} mb="5">
          {t("ordersPage.orderIn")}
        </Heading>
        <SegmentedControl.Root
          className={classNames.segmentedControl}
          defaultValue="PENDING"
          size="2"
          onValueChange={(value) => setSelectedStatus(value)}
          value={selectedStatus}
        >
          {status.map((item) => (
            <SegmentedControl.Item key={item.value} value={item.value}>
              {item.label()}
            </SegmentedControl.Item>
          ))}
        </SegmentedControl.Root>
        <Flex
          className={classNames.cardInItems}
          direction="column"
          gap="4"
          mt="5"
        >
          {orders
            .filter((item) => item.status === selectedStatus)
            .map((item) => (
              <OrderCard
                key={item.id}
                item={item}
                selected={selectedOrderId === item.id}
                onClick={() => setSelectedOrderId(item.id)}
              />
            ))}
        </Flex>
      </Card>
      {selectedOrder ? (
        <Card className={classNames.cardDetails} size="3">
          <Heading>{t("ordersPage.orderDetails")}</Heading>
          <Flex
            className={classNames.detailContent}
            direction="column"
            flexGrow="1"
          >
            <Flex direction="column" gap="4" flexGrow="1">
              <Flex direction="row" justify="between">
                <Flex direction="column">
                  <Text weight="bold">
                    {t("labels.orderNumber", {
                      number: selectedOrder.number,
                    })}
                  </Text>
                  <Text size="1" color="gray">
                    {selectedOrder.createdAt.toLocaleString()}
                  </Text>
                </Flex>
                <Flex direction="column">
                  <Text weight="bold">{selectedOrder.customer.name}</Text>
                  <Text size="1" color="gray">
                    User since{" "}
                    {selectedOrder.customer.createdAt.toLocaleString()}
                  </Text>
                </Flex>
              </Flex>
              <Separator orientation="horizontal" size="4" />
              <Flex direction="column" gap="6" flexGrow="1">
                {selectedOrder.items.map((orderItem) => (
                  <OrderItemCard key={orderItem.id} item={orderItem} />
                ))}
              </Flex>
              <Separator orientation="horizontal" size="4" />
              <Flex direction="row" justify="between">
                <Text weight="bold" size="4">
                  {t("labels.total")}
                </Text>
                <Text weight="bold" size="4">
                  <Text className={classNames.symbol}>
                    {t("labels.currency")}
                  </Text>
                  {selectedOrder.amount}
                </Text>
              </Flex>
            </Flex>
          </Flex>
          <Flex direction="row" justify="end" mt="4">
            <Button
              size="4"
              variant="outline"
              color="red"
              disabled={selectedOrder.status === "DELIVERED"}
            >
              {t("ordersPage.cancelOrder")}
            </Button>
          </Flex>
        </Card>
      ) : null}
    </Flex>
  );
};
