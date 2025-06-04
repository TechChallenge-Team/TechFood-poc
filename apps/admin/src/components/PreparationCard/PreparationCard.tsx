import { useEffect, useState } from "react";
import { Button, Card, Flex, Separator, Text } from "@radix-ui/themes";
import { PreparationItem } from "../../models";
import {
  formatDuration,
  getElapsedTime,
  getDurationColor,
} from "../../utilities";
import { t } from "../../i18n";
import { PreparationCardProps } from "./PreparationCard.types";

import classNames from "./PreparationCard.module.css";

const BUTTON_LABELS = {
  PENDING: () => t("preparationCard.start"),
  STARTED: () => t("preparationCard.finish"),
  READY: () => t("preparationCard.deliver"),
};

const STATUS = {
  PENDING: () => t("preparationCard.status.pending"),
  STARTED: () => t("preparationCard.status.started"),
  READY: () => t("preparationCard.status.delivered"),
};

const PreparationCardItem = ({ item }: { item: PreparationItem }) => {
  return (
    <Flex className={classNames.item} direction="row" gap="2" align="center">
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
  );
};

export const PreparationCard = ({
  item,
  onStart,
  onReady,
}: PreparationCardProps) => {
  const [waitingTime, setWaitingTime] = useState<number>(0);
  const [preparationTime, setPreparationTime] = useState<number>(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setWaitingTime(getElapsedTime(new Date(item.createdAt)));
      setPreparationTime(
        item.startedAt ? getElapsedTime(new Date(item.startedAt)) : 0
      );
    }, 1000);

    return () => clearInterval(interval);
  }, [item]);

  return (
    <Card className={classNames.root} size="3">
      <Flex direction="column" gap="4">
        <Flex direction="column" align="center">
          <Text weight="bold" size="4">
            {t("labels.orderNumber", { number: item.number })}
          </Text>
          <Text size="1" color="gray">
            {item.createdAt.toLocaleString()}
          </Text>
        </Flex>
        <Separator orientation="horizontal" size="4" />
        <Flex direction="column" gap="1">
          <Flex direction="row" justify="between" align="center">
            <Text weight="medium" size="2" color="gray">
              {t("preparationCard.waitingTime")}
            </Text>
            <Text weight="bold" size="2" color={getDurationColor(waitingTime)}>
              {formatDuration(waitingTime)}
            </Text>
          </Flex>
          <Flex direction="row" justify="between" align="center">
            <Text weight="medium" size="2" color="gray">
              {t("preparationCard.preparationTime")}
            </Text>
            <Text
              weight="bold"
              size="2"
              color={getDurationColor(preparationTime)}
            >
              {formatDuration(preparationTime)}
            </Text>
          </Flex>
          <Flex direction="row" justify="between" align="center">
            <Text weight="medium" size="2" color="gray">
              {t("labels.status")}
            </Text>
            <Text weight="bold" size="2" color="green">
              {STATUS[item.status]()}
            </Text>
          </Flex>
        </Flex>
        <Separator orientation="horizontal" size="4" />
        <Flex direction="column" gap="3">
          <Text weight="bold" size="4">
            {t("labels.items")}
          </Text>
          <Flex className={classNames.items} direction="column" gap="2">
            {item.items.map((dish) => (
              <PreparationCardItem key={dish.id} item={dish} />
            ))}
          </Flex>
        </Flex>
        <Separator orientation="horizontal" size="4" />
        <Button
          size="4"
          variant="outline"
          mt="2"
          color={item.status === "PENDING" ? "blue" : "green"}
          onClick={() => {
            if (item.status === "PENDING" && onStart) {
              onStart(item);
            } else if (item.status === "STARTED" && onReady) {
              onReady(item);
            }
          }}
        >
          {BUTTON_LABELS[item.status]()}
        </Button>
      </Flex>
    </Card>
  );
};
