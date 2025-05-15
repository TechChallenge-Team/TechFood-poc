import { Flex, Section } from "@radix-ui/themes";
import { t } from "../../i18n";
import { useState } from "react";
import { OrderCard } from "../../components";
import { OrderMonitor, OrderStatusType } from "../../models/OrderMonitor";

export const items: OrderMonitor[] = [
  {
    id: "a1b2c3",
    number: 1001,
    status: OrderStatusType.Received, // Criado
    products: [
      { id: "p1", name: "Café Expresso", quantity: 2 },
      { id: "p2", name: "Pão na Chapa", quantity: 1 },
    ],
  },
  {
    id: "d4e5f6",
    number: 1002,
    status: OrderStatusType.InPreparation, // Em Preparação
    products: [
      { id: "p3", name: "Suco de Laranja", quantity: 3 },
      { id: "p4", name: "Muffin de Chocolate", quantity: 2 },
    ],
  },
  {
    id: "g7h8i9",
    number: 1003,
    status: OrderStatusType.Done, // Pronto
    products: [{ id: "p5", name: "Sanduíche Natural", quantity: 1 }],
  },
  {
    id: "j1k2l3",
    number: 1004,
    status: OrderStatusType.Finished, // Finalizado
    products: [
      { id: "p6", name: "Água Mineral", quantity: 2 },
      { id: "p7", name: "Cookie de Aveia", quantity: 4 },
    ],
  },
  // mais exemplos...
  {
    id: "k4l5m6",
    number: 1005,
    status: OrderStatusType.Received,
    products: [
      { id: "p8", name: "Capuccino", quantity: 1 },
      { id: "p9", name: "Brownie de Chocolate", quantity: 2 },
      { id: "p10", name: "Suco Detox", quantity: 1 },
    ],
  },
  {
    id: "n7o8p9",
    number: 1006,
    status: OrderStatusType.InPreparation,
    products: [{ id: "p11", name: "Torrada Integral", quantity: 3 }],
  },
  {
    id: "q1r2s3",
    number: 1007,
    status: OrderStatusType.Done,
    products: [
      { id: "p12", name: "Pão de Queijo", quantity: 5 },
      { id: "p13", name: "Chá Gelado", quantity: 2 },
    ],
  },
  {
    id: "t4u5v6",
    number: 1008,
    status: OrderStatusType.Finished,
    products: [
      { id: "p14", name: "Salada de Frutas", quantity: 1 },
      { id: "p15", name: "Granola", quantity: 2 },
    ],
  },
];

export const Monitoring = () => {
  const [orders, setOrders] = useState<OrderMonitor[]>([]);
  // useEffect(
  //   const fetchProductsAndCategories = async () => {
  //     const [productsResponse, categoriesResponse] = await Promise.all([
  //       axios.get<OrderMonitor[]>("/api/v1/OrderMonitor"),
  //     ]);
  //     setProducts(productsResponse.data);
  //     setProductsFiltered(productsResponse.data);
  //     setCategories(categoriesResponse.data);
  //   };

  //   fetchProductsAndCategories();
  // )

  return (
    <Flex direction="column">
      <h1>Orders</h1>

      <Flex gap="3" title={t("MenuManagement.Monitoring")}>
        {items.map((order) => (
          <OrderCard key={order.id} orderMonitor={order} />
        ))}
      </Flex>
    </Flex>
  );
};
