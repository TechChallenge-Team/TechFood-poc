import { createBrowserRouter } from "react-router";
import { StartPage, MenuPage } from "./pages";

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
  ],
  {
    basename: "/self-order",
  }
);

export default router;
