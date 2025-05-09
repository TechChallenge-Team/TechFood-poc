import { createBrowserRouter } from "react-router";
import { StartPage, MenuPage, CheckoutPage, RegisterPage } from "./pages";


const router = createBrowserRouter(
  [
    {
      index: true,
      path: "/",
      element: <StartPage />,
    },
    {
      path: "menu",
      element: <MenuPage />,
    },
    {
      path: "checkout",
      element: <CheckoutPage />,
    },
    {
      path: "register/:doc",
      element: <RegisterPage />,
    },
  ],
  {
    basename: "/self-order",
  }
);

export default router;
