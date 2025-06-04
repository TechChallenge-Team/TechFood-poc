import { createBrowserRouter } from "react-router";
import { PreparationsPage } from "./pages";

const router = createBrowserRouter(
  [
    {
      index: true,
      path: "/",
      element: <PreparationsPage />,
    },
  ],
  {
    basename: "/monitor",
  }
);

export default router;
