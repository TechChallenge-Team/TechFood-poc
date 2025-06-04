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
  DashboardPage,
  ForbiddenPage,
  MenuManagementPage,
  OrdersPage,
  PreparationsPage,
  ReadyOrdersPage,
  SignInPage,
} from "./pages";

const router = createBrowserRouter(
  [
    {
      path: "/",
      element: <AdminLayout />,
      children: [
        {
          index: true,
          element: <DashboardPage />,
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
          element: <MenuManagementPage />,
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
      element: <SignInPage />,
    },
    {
      path: "/forbidden",
      element: <ForbiddenPage />,
    },
  ],
  {
    basename: "/admin",
  }
);

export default router;
