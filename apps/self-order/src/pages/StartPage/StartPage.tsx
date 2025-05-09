import { useState } from "react";
import { Box, Button, Flex, IconButton, TextField } from "@radix-ui/themes";
import { ArrowRightIcon } from "lucide-react";
import { t } from "../../i18n";
import { LanguageSwitch } from "../../components";

import classNames from "./StartPage.module.css";
import { Link, useNavigate } from "react-router";
import { validaCPF } from "../../utilities";

export const StartPage = () => {
  const [documentNumber, setDocumentNumber] = useState("");
  let navigate = useNavigate();
  function register() {
    //if (validaCPF(documentNumber)) {

    navigate('/register')
    //}

  }
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
          <Button onClick={register} size="3">{t("startPage.register")}</Button>
          <Link to='/menu' >
            <Button variant="outline" size="3">
              {t("startPage.dontIdentify")}
            </Button>
          </Link>
        </Flex>
      </Flex>
    </Flex>
  );
};
