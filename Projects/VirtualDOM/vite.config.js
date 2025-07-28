import { defineConfig } from 'vite'

export default defineConfig({
  esbuild: {
    loader: { '.js': 'jsx' },
    jsx: 'transform',
    jsxFactory: 'h',
    jsxFragment: 'Fragment',
  },
  server: {
    port: 3000,
  },
})
