import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { federation } from '@module-federation/vite';

export default defineConfig({
  plugins: [
    react(),
    federation({
      name: 'host',
      remotes: {
        dashboard: 'http://localhost:3001/assets/remoteEntry.js',
        profile: 'http://localhost:3002/assets/remoteEntry.js',
        shared_ui: 'http://localhost:3003/assets/remoteEntry.js',
      },
      shared: ['react', 'react-dom'],
    }),
  ],
  server: {
    port: 3000,
  },
});
