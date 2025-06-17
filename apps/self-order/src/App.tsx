import { Theme } from "@radix-ui/themes";
import { RouterProvider } from "react-router";
import { CustomerProvider, OrderProvider } from "./contexts";
import { ToastContainer } from "react-toastify";
import router from "./routes";

import "@radix-ui/themes/styles.css";
import "./App.css";

function App() {
  return (
    <Theme accentColor="amber" radius="large" grayColor="sage">
      <CustomerProvider>
        <OrderProvider>
          <RouterProvider router={router} />
        </OrderProvider>
      </CustomerProvider>
      <ToastContainer
        position="bottom-right"
        autoClose={3000}
        hideProgressBar={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
      />
    </Theme>
  );
}

export default App;
