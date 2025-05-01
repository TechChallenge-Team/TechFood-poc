export function configureServer() {
  return {
    server: {
      proxy: {
        "/api": {
          target: "https://localhost:44310",
          changeOrigin: true,
          secure: false,
          rewrite: (path) => path.replace(/^\/api/, ""),
        },
      },
    },
  };
}
