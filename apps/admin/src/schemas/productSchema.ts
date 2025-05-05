import { z } from "zod";

export const productSchema = z.object({
  name: z.string().min(1, "Nome é obrigatório"),
  description: z.string().min(1, "Descrição é obrigatória"),
  categoryId: z.string().min(1, "Categoria é obrigatória"),
  file: z
    .custom<FileList | undefined>()
    .refine((files) => files && files.length > 0, "Imagem é obrigatória")
    .refine((files) => {
      if (!files || files.length === 0) return false;
      const type = files[0].type;
      return ["image/jpeg", "image/png", "image/webp"].includes(type);
    }, "Formato inválido")
    .refine((files) => {
      if (!files || files.length === 0) return false;
      const size = files[0].size;
      return size <= 5 * 1024 * 1024;
    }, "Tamanho máximo é 5MB"),
  price: z
    .number({ invalid_type_error: "Preço é obrigatório" })
    .min(1, "Preço deve ser maior que zero")
    .optional()
    .refine((val) => val !== undefined, {
      message: "Preço é obrigatório",
    }),
});

export type ProductFormData = z.infer<typeof productSchema>;
