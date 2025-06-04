import { Flex } from "@radix-ui/themes";
import { useEffect, useState } from "react";
import { ReadyOrder } from "../../models";
import { ReadyOrderCard } from "../../components";
import { t } from "../../i18n";
import api from "../../api";

const INTERVAL = 5000; // 5s

export const ReadyOrdersPage = () => {
  const [readyOrders, setReadyOrders] = useState<ReadyOrder[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const { data } = await api.get<ReadyOrder[]>("/v1/orders/ready");

      setReadyOrders(data);
    };

    fetchData();

    const interval = setInterval(() => fetchData(), INTERVAL);
    return () => clearInterval(interval);
  }, []);

  const handleDeliver = async (item: ReadyOrder) => {
    await api.patch(`/v1/orders/${item.id}/deliver`);
    setReadyOrders((prev) => prev.filter((order) => order.id !== item.id));
  };

  return (
    <Flex gap="4" align="start" wrap="wrap">
      {readyOrders.length === 0 && <p>{t("readyOrdersPage.noReadyOrders")}</p>}
      {readyOrders.map((order) => (
        <ReadyOrderCard key={order.id} item={order} onDeliver={handleDeliver} />
      ))}
    </Flex>
  );
};
