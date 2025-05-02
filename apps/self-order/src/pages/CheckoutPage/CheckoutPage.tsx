import { useState } from "react";
import {
  Button,
  Flex,
  Heading,
  IconButton,
  Radio,
  Text,
} from "@radix-ui/themes";
import { ArrowLeftIcon } from "lucide-react";
import { QRCodeSVG } from "qrcode.react";
import { t } from "../../i18n";
import { BottomSheet } from "../../components";

import classNames from "./CheckoutPage.module.css";

const paymentMethods = [
  {
    id: "credit-card",
    name: () => t("checkoutPage.creditCard"),
    img: "credit-card",
  },
  { id: "pix", name: () => t("checkoutPage.pix"), img: "pix" },
];

const qrcode =
  "00020101021243650016COM.MERCADOLIBRE02013063638f1192a-5fd1-4180-a180-8bcae3556bc35204000053039865802BR5925IZABEL AAAA DE MELO6007BARUERI62070503***63040B6D";

const PaymentMethod = ({
  name,
  selected,
  img,
  onClick,
}: {
  name: string;
  selected: boolean;
  img: string;
  onClick: () => void;
}) => {
  const src = new URL(`../../assets/payments/${img}.png`, import.meta.url).href;
  return (
    <Flex
      className={classNames.paymentMethod}
      align="center"
      justify="between"
      onClick={onClick}
    >
      <Flex gap="3" align="center">
        <img src={src} />
        <Text>{name}</Text>
      </Flex>
      <Radio value={name} checked={selected} />
    </Flex>
  );
};

const SummaryItem = ({
  name,
  price,
  discount,
}: {
  name: string;
  price: string;
  discount?: boolean;
}) => {
  return (
    <Flex className={classNames.summaryItem} align="center" justify="between">
      <Text color="gray">{name}</Text>
      <Text color={discount ? "green" : undefined} weight="medium">
        {t("labels.currency")}
        {discount ? `-${price}` : price}
      </Text>
    </Flex>
  );
};

export const CheckoutPage = () => {
  const [isPaymentOpen, setIsPaymentOpen] = useState(false);
  const [selectedPaymentMethod, setSelectedPaymentMethod] = useState(
    paymentMethods[0].id
  );

  return (
    <Flex className={classNames.root} direction="column" align="center" gap="9">
      <Flex
        className={classNames.header}
        align="center"
        justify="between"
        gap="5"
      >
        <IconButton variant="outline" size="2" aria-label="Back">
          <ArrowLeftIcon size="30" />
        </IconButton>
        <Heading className={classNames.title}>
          {t("checkoutPage.title")}
        </Heading>
      </Flex>
      <Flex className={classNames.content} direction="column" gap="8">
        <Flex className={classNames.paymentMethods} direction="column" gap="4">
          {paymentMethods.map((method) => (
            <PaymentMethod
              key={method.id}
              name={method.name()}
              img={method.img}
              selected={method.id === selectedPaymentMethod}
              onClick={() => setSelectedPaymentMethod(method.id)}
            />
          ))}
        </Flex>
        <Flex className={classNames.orderSummary} direction="column" gap="4">
          <Heading size="2">{t("checkoutPage.orderSummary")}</Heading>
          <Flex
            className={classNames.orderSummaryContent}
            direction="column"
            gap="2"
          >
            <SummaryItem name={t("checkoutPage.subtotal")} price="50.00" />
            <SummaryItem
              name={t("checkoutPage.discount")}
              price="5.00"
              discount
            />
          </Flex>
          <Flex
            className={classNames.orderSummaryTotal}
            direction="row"
            gap="4"
            justify="between"
          >
            <Text>{t("labels.total")}</Text>
            <Text weight="medium">{t("labels.currency")}45.00</Text>
          </Flex>
        </Flex>
        <Button size="3" onClick={() => setIsPaymentOpen(true)}>
          {t("checkoutPage.pay")}
        </Button>
      </Flex>
      {isPaymentOpen && selectedPaymentMethod === "pix" && (
        <BottomSheet
          onClose={() => setIsPaymentOpen(false)}
          showCloseButton={false}
          closeOnOverlayClick={false}
        >
          <Flex direction="column" align="center" gap="4">
            <Flex direction="column" align="center">
              <Text size="2" weight="medium">
                {t("labels.total")}
              </Text>
              <Text size="4" weight="bold">
                {t("labels.currency")}45.00
              </Text>
            </Flex>
            <Text size="4" weight="medium" color="gray">
              {t("checkoutPage.pixInstructions")}
            </Text>
            <QRCodeSVG value={qrcode} size={350} />
          </Flex>
        </BottomSheet>
      )}
    </Flex>
  );
};
