import { createBrowserRouter } from "react-router";
import { StartPage } from "./pages";

const router = createBrowserRouter(
  [
    {
      index: true,
      path: "/",
      element: <StartPage />,
    },
  ],
  {
    basename: "/order-monitor",
  }
);

export default router;
