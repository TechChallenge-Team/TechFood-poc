import { Box, Button, Flex, TextField } from '@radix-ui/themes';
import { useEffect, useState } from 'react';
import { useParams } from "react-router"
import * as Label from "@radix-ui/react-label";
import { useForm } from 'react-hook-form';
import { z } from 'zod';
import { zodResolver } from "@hookform/resolvers/zod";
import axios from 'axios';
import { Link, useNavigate } from "react-router";

const createCustomerSchema = z.object({
  name: z.string().min(1, "O campo nome é obrigatório"),
  email: z.string().email("Formato de email inválido"),
});
type CreateCustomerSchema = z.infer<typeof createCustomerSchema>
export const RegisterPage = () => {
  const [documentNumber, setDocumentNumber] = useState("");
  let navigate = useNavigate();
  const { register, handleSubmit, formState: { errors } } = useForm<CreateCustomerSchema>({
    resolver: zodResolver(createCustomerSchema),
  });
  let params = useParams();
  let doc = params.doc;

  const onSubmit = async (customer: CreateCustomerSchema) => {

    try {
      const result = await axios.post<any>("/api/v1/Customers",
        { cpf: params.doc, name: customer.name, email: customer.email });
      console.log(result);
      console.log('ok!!!')
    } catch (error) {
      console.log('erro!!!');
      console.log(error);
    }





    //    navigate('/menu')



  };



  useEffect(() => {
    setDocumentNumber(String(params.doc))
  }, []);
  return (
    <Flex direction="column">
      <Flex direction="column" align="center" justify="center" flexGrow="1">
        <Flex direction="column" gap="4">
          <Flex gap="2" align="center">
            <Box maxWidth="400px">
              <form onSubmit={handleSubmit(onSubmit)}>
                <Label.Root className="label" htmlFor="cpf">
                  CPF:
                </Label.Root>
                <TextField.Root name='cpf' size="3" maxLength={11} value={documentNumber} disabled={true} />
                <Label.Root className="label" htmlFor="nome">
                  Nome:
                </Label.Root>
                <TextField.Root {...register("name", { required: "O campo nome é obrigatório" })}
                  size="3" maxLength={255} />
                {errors.name && <span>{errors.name.message}</span>}
                <Label.Root className="label" htmlFor="email">
                  Email:
                </Label.Root>
                <TextField.Root{...register("email")} size="3" maxLength={255} />
                {errors.email && <span>{errors.email.message}</span>}
                <Button type='submit' size="3"   >Cadastrar</Button>
              </form>
            </Box>
          </Flex>
        </Flex>
      </Flex>
    </Flex>


  );
}

