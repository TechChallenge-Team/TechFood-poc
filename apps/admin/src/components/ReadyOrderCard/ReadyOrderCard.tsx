import { useEffect, useState } from "react";
import { Button, Card, Flex, Separator, Text } from "@radix-ui/themes";
import {
  formatDuration,
  getElapsedTime,
  getDurationColor,
} from "../../utilities";
import { t } from "../../i18n";
import { ReadyOrderCardProps } from "./ReadyOrderCard.types";

import classNames from "./ReadyOrderCard.module.css";

export const ReadyOrderCard = ({ item, onDeliver }: ReadyOrderCardProps) => {
  const [waitingTime, setWaitingTime] = useState<number>(0);
  const [readyOrderTime, setReadyOrderTime] = useState<number>(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setWaitingTime(getElapsedTime(new Date(item.createdAt)));
      setReadyOrderTime(
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
              {t("readyOrderCard.waitingTime")}
            </Text>
            <Text weight="bold" size="2" color={getDurationColor(waitingTime)}>
              {formatDuration(waitingTime)}
            </Text>
          </Flex>
          <Flex direction="row" justify="between" align="center">
            <Text weight="medium" size="2" color="gray">
              {t("readyOrderCard.preparationTime")}
            </Text>
            <Text
              weight="bold"
              size="2"
              color={getDurationColor(readyOrderTime)}
            >
              {formatDuration(readyOrderTime)}
            </Text>
          </Flex>
        </Flex>
        <Separator orientation="horizontal" size="4" />
        <Button
          size="4"
          variant="outline"
          mt="2"
          color="green"
          onClick={() => onDeliver(item)}
        >
          {t("readyOrderCard.deliver")}
        </Button>
      </Flex>
    </Card>
  );
};
