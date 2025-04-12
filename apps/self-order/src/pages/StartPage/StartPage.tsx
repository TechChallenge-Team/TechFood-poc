import { useState } from "react";
import { Box, Button, Flex, IconButton, TextField } from "@radix-ui/themes";
import { ArrowRightIcon } from "lucide-react";
import { LanguageSwitch } from "../../components";

import classNames from "./StartPage.module.css";

export const StartPage = () => {
  const [cpf, setCpf] = useState("");

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
                placeholder="CPF"
                size="3"
                maxLength={11}
                onChange={(e) => setCpf(e.target.value)}
              />
            </Box>
            <IconButton size="3" disabled={cpf.length < 11}>
              <ArrowRightIcon size="40" />
            </IconButton>
          </Flex>
          <Button size="3">Cadastre-se</Button>
          <Button variant="outline" size="3">
            Não se indentificar
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};
