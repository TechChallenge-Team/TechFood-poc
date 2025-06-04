import { Flex } from "@radix-ui/themes";
import { useEffect, useState } from "react";
import api from "../../api";
import { PreparationCard } from "../../components";
import { Preparation } from "../../models";
import { t } from "../../i18n";

const INTERVAL = 5000; // 5s

export const PreparationsPage = () => {
  const [preparations, setPreparations] = useState<Preparation[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      const { data } = await api.get<Preparation[]>("/v1/preparations");

      setPreparations(data);
    };

    fetchData();

    const interval = setInterval(() => fetchData(), INTERVAL);
    return () => clearInterval(interval);
  }, []);

  const handleStart = async (item: Preparation) => {
    await api.patch(`/v1/preparations/${item.id}/start`);
    setPreparations((prev) =>
      prev.map((prep) =>
        prep.id === item.id
          ? { ...prep, status: "STARTED", startedAt: new Date() }
          : prep
      )
    );
  };

  const handleReady = async (item: Preparation) => {
    await api.patch(`/v1/preparations/${item.id}/complete`);
    setPreparations((prev) => prev.filter((prep) => prep.id !== item.id));
  };

  return (
    <Flex gap="4" align="start" wrap="wrap">
      {preparations.length === 0 && <p>{t("preparationsPage.noOrders")}</p>}
      {preparations.map((prep) => (
        <PreparationCard
          key={prep.id}
          item={prep}
          onReady={handleReady}
          onStart={handleStart}
        />
      ))}
    </Flex>
  );
};
