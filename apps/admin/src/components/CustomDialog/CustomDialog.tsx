import { t } from "../../i18n";
import { AlertDialog, Button, Flex } from "@radix-ui/themes";

interface ICustomDialogProps {
  title: string;
  description: string;
  dialogOpen: boolean;
  setDialogOpen: (open: boolean) => void;
  onConfirm: () => void;
}

export const CustomDialog = ({
  title,
  description,
  dialogOpen,
  setDialogOpen,
  onConfirm,
}: ICustomDialogProps) => {
  return (
    <AlertDialog.Root open={dialogOpen} onOpenChange={setDialogOpen}>
      <AlertDialog.Content maxWidth="450px">
        <AlertDialog.Title>{title}</AlertDialog.Title>
        <AlertDialog.Description size="3">
          {description}
        </AlertDialog.Description>
        <Flex gap="3" mt="4" justify="end">
          <AlertDialog.Cancel>
            <Button variant="soft" color="gray">
              {t("DeleteDialog.Cancel")}
            </Button>
          </AlertDialog.Cancel>
          <AlertDialog.Action>
            <Button variant="solid" color="red" onClick={() => onConfirm()}>
              {t("DeleteDialog.Confirm")}
            </Button>
          </AlertDialog.Action>
        </Flex>
      </AlertDialog.Content>
    </AlertDialog.Root>
  );
};
