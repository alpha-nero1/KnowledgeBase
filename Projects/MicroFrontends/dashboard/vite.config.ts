import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { federation } from '@module-federation/vite';
import path from 'path';

export default defineConfig({
  plugins: [
    react(),
    federation({
      name: 'dashboard',
      filename: 'remoteEntry.js',
      exposes: {
        './Dashboard': './src/Dashboard.tsx',
      },
      shared: ['react', 'react-dom'],
      remotes: {
        shared_ui: 'http://localhost:3003/assets/remoteEntry.js',
      },
    }),
  ],
  resolve: {
    alias: {
      '@shared_ui': path.resolve(__dirname, '../shared_ui')
    }
  },
  server: {
    port: 3001,
  },
});
