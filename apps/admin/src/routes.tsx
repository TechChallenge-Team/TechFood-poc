import { createBrowserRouter } from "react-router";
import {
  CookingPotIcon,
  ListTodoIcon,
  MonitorDotIcon,
  SquareKanbanIcon,
  UtensilsIcon,
} from "lucide-react";
import { AdminLayout } from "./components";
import {
  Dashboard,
  Forbidden,
  MenuManagement,
  OrdersPage,
  PreparationsPage,
  ReadyOrdersPage,
  SignIn,
} from "./pages";

const router = createBrowserRouter(
  [
    {
      path: "/",
      element: <AdminLayout />,
      children: [
        {
          index: true,
          element: <Dashboard />,
          handle: {
            title: "Dashboard",
            menu: true,
            roles: ["admin"],
            icon: <SquareKanbanIcon />,
          },
        },
        {
          path: "menu",
          handle: {
            title: "Menu",
            menu: true,
            roles: ["admin"],
            icon: <UtensilsIcon />,
          },
          element: <MenuManagement />,
        },
        {
          path: "orders",
          element: <OrdersPage />,
          handle: { title: "Orders", menu: true, icon: <MonitorDotIcon /> },
        },
        {
          path: "preparations",
          element: <PreparationsPage />,
          handle: {
            title: "Preparations",
            menu: true,
            icon: <CookingPotIcon />,
          },
        },
        {
          path: "ready-orders",
          element: <ReadyOrdersPage />,
          handle: { title: "Ready Orders", menu: true, icon: <ListTodoIcon /> },
        },
        // {
        //   path: "reviews",
        //   element: <MenuManagement />,
        //   handle: { title: "Reviews", menu: true, icon: <StarIcon /> },
        // },
      ],
    },
    {
      path: "/signin",
      element: <SignIn />,
    },
    {
      path: "/forbidden",
      element: <Forbidden />,
    },
  ],
  {
    basename: "/admin",
  }
);

export default router;
