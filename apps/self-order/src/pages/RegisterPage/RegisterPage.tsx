import { Flex, TextField } from '@radix-ui/themes';


export const RegisterPage = () => {
  return (
    <Flex direction="column">
      <TextField.Root

        size="3"
        maxLength={11}

      />
    </Flex>


  );
}