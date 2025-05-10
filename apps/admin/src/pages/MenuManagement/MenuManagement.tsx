import { Flex, Heading, TextField, Button } from "@radix-ui/themes";
import { MagnifyingGlassIcon } from "@radix-ui/react-icons";
import { CategoryCard, ProductCard, ProductCardProps } from "../../components";
import { ProductModal } from "../../components/ProductModal";
import { Category } from "../../models/Category";
import { t } from "../../i18n";
import { normalizeText } from "../../utilities/normalizeText";
import { CustomDialog } from "../../components/CustomDialog";
import classNames from "./MenuManagement.module.css";
import { useEffect, useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import { Product } from "../../models/Product";

const Section = ({ title, direction, children }: any) => {
  return (
    <Flex className={classNames.section} direction="column" gap="4">
      <Heading size="4" as="h2">
        {title}
      </Heading>
      <Flex gap="4" wrap="wrap" direction={direction}>
        {children}
      </Flex>
    </Flex>
  );
};

export const MenuManagement = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [productsFiltered, setProductsFiltered] = useState<Product[]>([]);
  const [productFormIsOpen, setProductFormIsOpen] = useState(false);
  const [search, setSearch] = useState("");
  const [categories, setCategories] = useState<Category[]>([]);
  const [categorySelected, setCategorySelected] = useState("");
  const [dialogDeleteOpen, setDialogDeleteOpen] = useState(false);
  const [selectedDeleteProduct, setSelectedDeleteProduct] =
    useState<string>("");
  const [selectedEditProduct, setSelectedEditProduct] =
    useState<Product | null>(null);

  const handleFilterByCategory = (category: string) => {
    if (category === categorySelected) {
      setProductsFiltered(products);
      setCategorySelected("");
      return;
    }

    const filtered = products.filter(
      (product) => product.categoryId === category
    );
    setProductsFiltered(filtered);
    setCategorySelected(category);
  };

  const handleEditProduct = async (formData: FormData) => {
    const result = await axios.put("/api/v1/Products", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (result.status === 200) {
      toast.success(t("ProductModal.SuccessMessage"));
      setProductFormIsOpen(false);
    } else {
      toast.error(t("ProductModal.ErrorMessage"));
    }

    setProducts((prev) => [...prev, result.data]);
    setProductsFiltered((prev) => [...prev, result.data]);
  };

  const handleProduct = async (formData: FormData) => {
    const result = await axios.post("/api/v1/Products", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    if (result.status === 200) {
      toast.success(t("ProductModal.SuccessMessage"));
      setProductFormIsOpen(false);
    } else {
      toast.error(t("ProductModal.ErrorMessage"));
    }

    setProducts((prev) => [...prev, result.data]);
    setProductsFiltered((prev) => [...prev, result.data]);
  };

  const handleOpenDeleteAlertDialog = (id: string) => {
    setSelectedDeleteProduct(id);
    setDialogDeleteOpen(true);
  };

  const handleOpenEditDialog = (product: Product) => {
    setSelectedEditProduct(product);
    setProductFormIsOpen(true);
  };

  const handleDeleteProduct = async () => {
    if (selectedDeleteProduct) {
      const result = await axios.delete(
        `/api/v1/Products/${selectedDeleteProduct}`
      );
      setSelectedDeleteProduct("");
      setDialogDeleteOpen(false);

      if (result.status !== 204) {
        toast.error("Error deleting product");
        return;
      }

      toast.success(t("ProductModal.DeleteSuccessMessage"));
      setProducts((prev) =>
        prev.filter((product) => product.id !== selectedDeleteProduct)
      );
      setProductsFiltered((prev) =>
        prev.filter((product) => product.id !== selectedDeleteProduct)
      );
    }
  };

  useEffect(() => {
    const filtered = products.filter((product) =>
      normalizeText(product.name).includes(normalizeText(search))
    );
    setProductsFiltered(filtered);
    setCategorySelected("");
  }, [search, products]);

  useEffect(() => {
    const fetchProductsAndCategories = async () => {
      const [productsResponse, categoriesResponse] = await Promise.all([
        axios.get<Product[]>("/api/v1/Products"),
        axios.get<Category[]>("/api/v1/Categories"),
      ]);
      setProducts(productsResponse.data);
      setProductsFiltered(productsResponse.data);
      setCategories(categoriesResponse.data);
    };

    fetchProductsAndCategories();
  }, []);

  return (
    <Flex direction="column">
      <Flex gap="8" align="center">
        <TextField.Root
          className={classNames.search}
          placeholder="Search"
          size="3"
          onChange={(e) => {
            setSearch(e.target.value);
          }}
        >
          <TextField.Slot>
            <MagnifyingGlassIcon height="25" width="25" />
          </TextField.Slot>
        </TextField.Root>
        <Button size={"3"} onClick={() => setProductFormIsOpen(true)}>
          {t("MenuManagement.AddProduct")}
        </Button>
      </Flex>
      <Flex direction="row" justify="between"></Flex>
      {!search && (
        <Section title={t("MenuManagement.Categories")}>
          {categories.map((category) => (
            <CategoryCard
              key={category.id}
              {...category}
              selected={categorySelected === category.id}
              handleFilterByCategory={handleFilterByCategory}
            />
          ))}
        </Section>
      )}
      <Section title={t("MenuManagement.Products")}>
        {productsFiltered.map((product) => (
          <ProductCard
            key={product.id}
            product={product}
            handleOpenDeleteAlertDialog={handleOpenDeleteAlertDialog}
            handleOpenEditDialog={handleOpenEditDialog}
          />
        ))}
      </Section>
      <CustomDialog
        title={t("DeleteDialog.Title")}
        description={t("DeleteDialog.Message")}
        dialogOpen={dialogDeleteOpen}
        setDialogOpen={setDialogDeleteOpen}
        onConfirm={handleDeleteProduct}
      />
      <ProductModal
        isOpen={productFormIsOpen}
        setIsOpen={setProductFormIsOpen}
        categories={categories}
        handleProduct={handleProduct}
        selectedEditProduct={selectedEditProduct}
      />
    </Flex>
  );
};
