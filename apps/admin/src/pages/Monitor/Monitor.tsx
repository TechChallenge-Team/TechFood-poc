import {
  Button,
  Flex,
  Heading,
  SegmentedControl,
  Text,
} from "@radix-ui/themes";
import { t } from "../../i18n";
import { useEffect, useState } from "react";
import { OrderMonitor, OrderStatus } from "../../models/OrderMonitor";
import classNames from "./Monitor.module.css";
import { OrderItem } from "./OrderCard";
import axios from "axios";

const segments = [
  { key: "PAID", value: "Received" },
  { key: "INPREPARATION", value: "In-Preparation" },
  { key: "DONE", value: "Done" },
];

type IOrderInformation = {
  order: OrderMonitor | null;
  updateOrderStatus: (id: string, status: OrderStatus) => any;
};

const OrderInformation = ({ order, updateOrderStatus }: IOrderInformation) => {
  const redButtonName = () =>
    order?.status === "INPREPARATION" || order?.status === "DONE"
      ? "Cancel"
      : "Reject";

  const greenButtonName = () => {
    if (order?.status === "INPREPARATION") return "Finish";
    else if (order?.status === "DONE") return "Delivered";
    else return "Accept";
  };

  const greenButtonUpdateAction = () => {
    if (order?.orderId == null) return;

    let status: OrderStatus = "INPREPARATION";
    if (order?.status === "INPREPARATION") status = "DONE";
    else if (order?.status === "DONE") status = "FINISH";

    updateOrderStatus(order?.orderId, status);
  };

  const redButtonUpdateAction = () => {
    if (!order?.orderId) return;
    updateOrderStatus(order?.orderId, "REJECT");
  };

  return (
    <Flex className={classNames.orderDescribe} direction="column" gap="4">
      <Flex justify="between" align="center">
        <Heading size="5" weight="bold">
          Order #{order?.number}
        </Heading>
        <Flex gap="3">
          <Button onClick={redButtonUpdateAction} color="red" size="3">
            {redButtonName()}
          </Button>
          <Button onClick={greenButtonUpdateAction} size="3">
            {greenButtonName()}
          </Button>
        </Flex>
      </Flex>

      <hr style={{ width: "100%", borderTop: "1px solid #ccc" }} />

      <Flex direction="column" gap="3">
        <Heading size="5">Items</Heading>

        {order?.products.map((x) => (
          <Flex
            className={classNames.product}
            key={x.name}
            gap="5"
            align="center"
          >
            <Flex className={classNames.imageContainer}>
              <img src={x.imageUrl} alt={x.name} />
            </Flex>
            <Flex direction="column">
              <Text>Name: {x.name}</Text>
              <Text>Quantity: {x.quantity}</Text>
            </Flex>
          </Flex>
        ))}
      </Flex>
    </Flex>
  );
};

export const Monitor = () => {
  const [orderList, setOrderList] = useState<OrderMonitor[]>([]);
  const [statusOrder, setStatusOrder] = useState<string>("CREATED");
  const [orderItem, setOrderItem] = useState<OrderMonitor | null>(null);

  const handleUpdateOrderStatus = (id: string, status: OrderStatus) => {
    const updatedList = orderList.map((order) =>
      order.orderId === id ? { ...order, status } : order
    );

    setOrderList(updatedList);
  };

  useEffect(() => {
    const fetchProducts = async () => {
      const response = await axios.get<OrderMonitor[]>(
        "/api/v1/Preparations/orders"
      );
      setOrderList(response.data);
    };

    fetchProducts();
  }, []);

  return (
    <Flex className={classNames.root} gap="4">
      <Flex className={classNames.orderIn} direction="column" gap="4">
        <Heading>Order In</Heading>

        <SegmentedControl.Root
          onValueChange={setStatusOrder}
          defaultValue="CREATED"
        >
          {segments.map((x) => (
            <SegmentedControl.Item key={x.key} value={x.key}>
              {x.value}
            </SegmentedControl.Item>
          ))}
        </SegmentedControl.Root>

        <Flex direction="column" className={classNames.orders} gap="4">
          {orderList
            .filter((x) => x.status == statusOrder)
            .map((order) => (
              <OrderItem
                key={order.orderId}
                orderMonitor={order}
                onClick={() => setOrderItem(order)}
              />
            ))}
        </Flex>
      </Flex>

      <Flex className={classNames.details} direction="column" gap="4">
        <Heading>Order Information</Heading>

        {orderItem && (
          <OrderInformation
            order={orderItem}
            updateOrderStatus={handleUpdateOrderStatus}
          />
        )}
      </Flex>
    </Flex>
  );
};
