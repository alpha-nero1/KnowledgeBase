import { defineConfig } from 'vite'

export default defineConfig({
  build: {
    rollupOptions: {
      input: 'index.js',
      output: {
        entryFileNames: 'index.js',
        format: 'iife', // Immediately Invoked Function Expression - works without modules
        name: 'vero' // Global variable name
      }
    },
    outDir: 'dist'
  },
  esbuild: {
    loader: { '.js': 'jsx' },
    jsx: 'transform',
    jsxFactory: 'vero',
    jsxFragment: 'Fragment',
  },
  server: {
    port: 3000,
  },
})
