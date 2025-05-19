import { createBrowserRouter } from "react-router";
import {
  SettingsIcon,
  SquareKanbanIcon,
  StarIcon,
  UtensilsIcon,
} from "lucide-react";
import { AdminLayout } from "./components";
import { Dashboard, Forbidden, MenuManagement, SignIn } from "./pages";

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
