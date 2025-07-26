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
      shared: ['react', 'react-dom']
    }),
  ],
  resolve: {
    alias: {
      '@shared-ui': path.resolve(__dirname, '../shared-ui')
    }
  },
  server: {
    port: 3001,
  },
});
