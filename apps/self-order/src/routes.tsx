import { createBrowserRouter } from "react-router";
import { StartPage, MenuPage, CheckoutPage } from "./pages";

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
  ],
  {
    basename: "/self-order",
  }
);

export default router;
