import { defineConfig } from "vite";
import { VitePWA } from "vite-plugin-pwa";
import react from "@vitejs/plugin-react";
import { configureCss } from "../../scripts";

export default defineConfig({
  base: "/admin",
  ...configureCss(),
  plugins: [
    react(),
    VitePWA({
      registerType: "autoUpdate",
      manifest: false,
    }),
  ],
});
