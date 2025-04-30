import { createBrowserRouter } from "react-router";
import {
  SettingsIcon,
  SquareKanbanIcon,
  StarIcon,
  UtensilsIcon,
} from "lucide-react";
import { AdminLayout } from "./components";
import { Dashboard, MenuManagement } from "./pages";

const router = createBrowserRouter(
  [
    {
      path: "/",
      Component: AdminLayout,
      children: [
        {
          index: true,
          element: <Dashboard />,
          handle: {
            title: "Dashboard",
            menu: true,
            icon: <SquareKanbanIcon />,
          },
        },
        {
          path: "menu",
          element: <MenuManagement />,
          handle: { title: "Menu", menu: true, icon: <UtensilsIcon /> },
        },
        {
          path: "reviews",
          element: <MenuManagement />,
          handle: { title: "Reviews", menu: true, icon: <StarIcon /> },
        },
        {
          path: "settings",
          element: <MenuManagement />,
          handle: { title: "Settings", menu: true, icon: <SettingsIcon /> },
        },
      ],
    },
  ],
  {
    basename: "/admin",
  }
);

export default router;
