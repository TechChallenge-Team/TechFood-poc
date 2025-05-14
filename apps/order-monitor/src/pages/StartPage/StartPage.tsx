import { useEffect, useState } from "react";
import { Flex } from "@radix-ui/themes";
import { TOrder } from "../../models/TOrder";
import { t } from "../../i18n";

import classNames from "./StartPage.module.css";
import axios from "axios";
import { OrderCard } from "../../components/OrderCard/OrderCard";

export const StartPage = () => {
  const [orders, setOrders] = useState<TOrder[]>([]);

  useEffect(() => {
    const timer = setInterval(async () => {
      const response = await axios.get("/api/v1/orders/done-and-preparation");
      setOrders(response.data);
    }, 2000);
    return () => clearTimeout(timer);
  }, []);

  const inPreparation = orders.filter(
    (order) => order.status === "INPREPARATION"
  );
  const done = orders.filter((order) => order.status === "DONE");

  const cardConfigs = [
    {
      key: "preparing",
      title: t("orderCard.inPreparation"),
      orders: inPreparation,
    },
    { key: "done", title: t("orderCard.done"), orders: done },
  ];
  return (
    <Flex className={classNames.root} direction="column">
      <Flex gap="2">
        {cardConfigs.map(({ key, title, orders }) => (
          <OrderCard key={key} type={key} title={title} orders={orders} />
        ))}
      </Flex>
    </Flex>
  );
};
