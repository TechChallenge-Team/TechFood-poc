import { useEffect, useState } from "react";
import {
  AlertDialog,
  Box,
  Button,
  Flex,
  IconButton,
  TextField,
} from "@radix-ui/themes";
import { ArrowRightIcon } from "lucide-react";
import { useNavigate } from "react-router";
import { t } from "../../i18n";
import { LanguageSwitch } from "../../components";
import { validateCPF } from "../../utilities";

import classNames from "./StartPage.module.css";
import { useOrder } from "../../contexts";

export const StartPage = () => {
  const [errorMessage, setErrorMessage] = useState("");
  const [documentNumber, setDocumentNumber] = useState("");

  const navigate = useNavigate();

  const { clearOrder } = useOrder();

  useEffect(() => {
    clearOrder();
  }, [clearOrder]);

  const handleRegister = () => {
    if (validateCPF(documentNumber)) {
      navigate("/register/" + documentNumber);
      return;
    }

    setErrorMessage(t("startPage.invalidDocument"));
  };

  const handlerDontIdentify = () => {
    navigate("/menu");
  };

  return (
    <Flex className={classNames.root} direction="column">
      <Flex className={classNames.languageSwitch} justify="end">
        <LanguageSwitch />
      </Flex>
      <Flex direction="column" align="center" justify="center" flexGrow="1">
        <Flex direction="column" gap="4">
          <Flex gap="2" align="center">
            <Box maxWidth="400px">
              <TextField.Root
                placeholder={t("startPage.documentNumber")}
                size="3"
                maxLength={11}
                onChange={(e) => setDocumentNumber(e.target.value)}
              />
            </Box>
            <IconButton size="3" disabled={documentNumber.length < 11}>
              <ArrowRightIcon size="40" />
            </IconButton>
          </Flex>
          <Button
            onClick={handleRegister}
            size="3"
            disabled={documentNumber.length < 11}
          >
            {t("startPage.register")}
          </Button>
          <Button onClick={handlerDontIdentify} variant="outline" size="3">
            {t("startPage.dontIdentify")}
          </Button>
        </Flex>
      </Flex>
      {errorMessage && (
        <AlertDialog.Root open={true}>
          <AlertDialog.Content>
            <AlertDialog.Description mb="5">
              <Flex justify="center">{errorMessage}</Flex>
            </AlertDialog.Description>
            <Flex gap="3" justify="center">
              <AlertDialog.Cancel onClick={() => setErrorMessage("")}>
                <Button variant="soft" color="gray">
                  {t("labels.close")}
                </Button>
              </AlertDialog.Cancel>
            </Flex>
          </AlertDialog.Content>
        </AlertDialog.Root>
      )}
    </Flex>
  );
};
