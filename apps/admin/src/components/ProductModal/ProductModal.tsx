import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as Label from "@radix-ui/react-label";
import {
  TextField,
  Flex,
  Text,
  Button,
  Dialog,
  Box,
  TextArea,
  Select,
} from "@radix-ui/themes";
import { productSchema, ProductFormData } from "../../schemas/productSchema";
import { FileInputWithPreview } from "../FileInputWithPreview/FileInputWithPreview";
import { CurrencyInput } from "../CurrencyInput/CurrencyInput";
import { Category } from "../../models/Category";
import classNames from "./ProductModal.module.css";
import { Product } from "../../models/Product";
import { useEffect } from "react";
import { t } from "../../i18n";

interface IProductModal {
  isOpen: boolean;
  categories: Category[];
  setIsOpen: (open: boolean) => void;
  handleProduct: (formData: FormData, id: string) => void;
  selectedEditProduct?: Product | null;
}

export const ProductModal = ({
  categories,
  handleProduct,
  isOpen,
  setIsOpen,
  selectedEditProduct,
}: IProductModal) => {
  const {
    register,
    handleSubmit,
    getValues,
    control,
    reset,
    formState: { errors },
  } = useForm<ProductFormData>({
    resolver: zodResolver(productSchema),
    defaultValues: {
      name: "",
      description: "",
      categoryId: "",
      file: undefined,
      price: undefined,
      imageUrl: "",
    },
  });

  useEffect(() => {
    if (selectedEditProduct) {
      reset({
        name: selectedEditProduct.name,
        description: selectedEditProduct.description,
        categoryId: selectedEditProduct.categoryId,
        file: undefined,
        price: selectedEditProduct.price,
        imageUrl: selectedEditProduct.imageUrl,
      });
    } else {
      reset({
        name: "",
        description: "",
        categoryId: "",
        file: undefined,
        price: undefined,
        imageUrl: "",
      });
    }
  }, [selectedEditProduct, reset]);

  const onSubmit = async (data: ProductFormData) => {
    const formData = new FormData();
    formData.append("Name", data.name);
    formData.append("Description", data.description);
    formData.append("CategoryId", data.categoryId);
    formData.append("Price", String(data.price));
    formData.append("File", data.file ? data.file[0] : "");

    await handleProduct(formData, selectedEditProduct?.id || "");
  };

  return (
    <Dialog.Root open={isOpen} onOpenChange={setIsOpen}>
      <Dialog.Content maxWidth="450px">
        <form onSubmit={handleSubmit(onSubmit)}>
          <Dialog.Title>
            {selectedEditProduct
              ? t("ProductModal.EditTitle")
              : t("ProductModal.AddTitle")}
          </Dialog.Title>
          <Flex direction="column" gap="3">
            <Box>
              <Label.Root htmlFor="name">{t("ProductModal.Name")}</Label.Root>
              <TextField.Root id="name" {...register("name")} />
              {errors.name && <Text color="red">{errors.name.message}</Text>}
            </Box>

            <Box>
              <Label.Root htmlFor="description">
                {t("ProductModal.Description")}
              </Label.Root>
              <Controller
                control={control}
                name="description"
                render={({ field }) => <TextArea id="description" {...field} />}
              />
              {errors.description && (
                <Text color="red">{errors.description.message}</Text>
              )}
            </Box>

            <Flex direction="column">
              <Controller
                control={control}
                name="categoryId"
                render={({ field }) => (
                  <>
                    <Label.Root htmlFor="categoryId">
                      {t("ProductModal.Category")}
                    </Label.Root>
                    <Select.Root
                      onValueChange={field.onChange}
                      value={field.value}
                    >
                      <Select.Trigger
                        className={classNames.selectButton}
                        placeholder={t("ProductModal.SelectCategory")}
                      />
                      <Select.Content position="popper">
                        <Select.Group>
                          {categories.map((category) => (
                            <Select.Item key={category.id} value={category.id}>
                              {category.name}
                            </Select.Item>
                          ))}
                        </Select.Group>
                      </Select.Content>
                    </Select.Root>
                  </>
                )}
              />
              {errors.categoryId && (
                <Text color="red">{errors.categoryId.message}</Text>
              )}
            </Flex>

            <Box>
              <Label.Root htmlFor="price">{t("ProductModal.Price")}</Label.Root>
              <Controller
                control={control}
                name="price"
                defaultValue={undefined}
                render={({ field }) => (
                  <CurrencyInput
                    id="price"
                    name="price"
                    value={field.value}
                    onChange={field.onChange}
                    error={errors.price?.message}
                  />
                )}
              />
            </Box>

            <Box>
              <Controller
                control={control}
                name="file"
                render={({ field }) => (
                  <FileInputWithPreview
                    name="file"
                    value={field.value}
                    onChange={field.onChange}
                    error={errors.file}
                    imageUrl={getValues("imageUrl")}
                  />
                )}
              />
            </Box>

            <Flex gap="3" mt="4" justify="end">
              <Dialog.Close>
                <Button variant="soft" color="gray">
                  {t("ProductModal.Cancel")}
                </Button>
              </Dialog.Close>
              <Button type="submit">{t("ProductModal.Save")}</Button>
            </Flex>
          </Flex>
        </form>
      </Dialog.Content>
    </Dialog.Root>
  );
};
