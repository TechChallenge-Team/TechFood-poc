import { DropdownMenu, IconButton, Box } from "@radix-ui/themes";
import { useI18n } from "../../i18n";

import classNames from "./LanguageSwitch.module.css";

const assetsPath = "../../assets/flags/";

export const LanguageSwitch = () => {
  const { langCode, setLanguage, languages } = useI18n();

  const changeLanguage = (code: any) => {
    setLanguage(code);
  };

  const langSrc = new URL(`${assetsPath}${langCode}.svg`, import.meta.url).href;

  const langs = languages.map(({ code, label }: any) => ({
    code,
    label,
    src: new URL(`${assetsPath}${code}.svg`, import.meta.url).href,
  }));

  return (
    <Box className={classNames.root}>
      <DropdownMenu.Root>
        <DropdownMenu.Trigger>
          <IconButton
            className={classNames.trigger}
            variant="ghost"
            radius="full"
            size="3"
          >
            <img src={langSrc} />
          </IconButton>
        </DropdownMenu.Trigger>
        <DropdownMenu.Content>
          {langs.map(({ code, label, src }) => (
            <DropdownMenu.Item
              key={code}
              className={classNames.option}
              onClick={() => changeLanguage(code)}
            >
              <img src={src} />
              {label}
            </DropdownMenu.Item>
          ))}
        </DropdownMenu.Content>
      </DropdownMenu.Root>
    </Box>
  );
};
