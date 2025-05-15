import { createBrowserRouter } from "react-router";
import { MonitorDot, SquareKanbanIcon, UtensilsIcon } from "lucide-react";
import { AdminLayout } from "./components";
import { Dashboard, MenuManagement } from "./pages";
import { Monitoring } from "./pages/Monitoring";

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
          path: "monitoring",
          element: <Monitoring />,
          handle: { title: "Monitoring", menu: true, icon: <MonitorDot /> },
        },
        // {
        //   path: "reviews",
        //   element: <MenuManagement />,
        //   handle: { title: "Reviews", menu: true, icon: <StarIcon /> },
        // },
      ],
    },
  ],
  {
    basename: "/admin",
  }
);

export default router;
