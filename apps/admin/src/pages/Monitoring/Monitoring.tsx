import { Flex, Section } from "@radix-ui/themes";
import { t } from "../../i18n";
import { useState } from "react";
import { OrderMonitor, OrderStatusType } from "../../models/OrderMonitor";
import { OrderCard } from "../../components";

export const items: OrderMonitor[] = [
  {
    id: "a1b2c3",
    number: 1001,
    status: OrderStatusType.Received,
    products: [
      { id: "p1", name: "Café Expresso", quantity: 2 },
      { id: "p2", name: "Pão na Chapa", quantity: 1 },
    ],
  },
  {
    id: "d4e5f6",
    number: 1002,
    status: OrderStatusType.InPreparation,
    products: [{ id: "p3", name: "Suco de Laranja", quantity: 3 }],
  },
  {
    id: "g7h8i9",
    number: 1003,
    status: OrderStatusType.Done,
    products: [],
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

      <Section title={t("MenuManagement.Products")}>
        {items.map((order) => (
          <OrderCard key={order.id} orderMonitor={order} />
        ))}
      </Section>
    </Flex>
  );
};
