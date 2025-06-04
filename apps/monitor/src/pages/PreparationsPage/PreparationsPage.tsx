import { useEffect, useState } from "react";
import { Flex } from "@radix-ui/themes";
import axios from "axios";
import { Preparation } from "../../models";
import { t } from "../../i18n";
import { PreparationsCard } from "../../components";

import classNames from "./PreparationsPage.module.css";

const STATUS = {
  PENDING: "PENDING",
  STARTED: "STARTED",
  READY: "READY",
};

const INTERVAL = 2000;

export const PreparationsPage = () => {
  const [preparations, setPreparations] = useState<Preparation[]>([]);

  useEffect(() => {
    const timer = setInterval(async () => {
      const response = await axios.get("/api/v1/preparations/tracking");
      setPreparations(response.data);
    }, INTERVAL);

    return () => clearTimeout(timer);
  }, []);

  const pendingItems = preparations.filter(
    (prep) => prep.status === STATUS.PENDING || prep.status === STATUS.STARTED
  );

  const completedItems = preparations.filter(
    (prep) => prep.status === STATUS.READY
  );

  const cardConfigs: {
    type: "preparing" | "done";
    title: string;
    items: Preparation[];
  }[] = [
    {
      type: "preparing",
      title: t("preparationsPage.inPreparation"),
      items: pendingItems,
    },
    { type: "done", title: t("preparationsPage.done"), items: completedItems },
  ];

  return (
    <Flex className={classNames.root} direction="column">
      <Flex gap="2">
        {cardConfigs.map(({ type, title, items }, i) => (
          <PreparationsCard key={i} type={type} title={title} items={items} />
        ))}
      </Flex>
    </Flex>
  );
};
