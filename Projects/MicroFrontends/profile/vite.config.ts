import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { federation } from '@module-federation/vite';
import path from 'path';

export default defineConfig({
  plugins: [
    react(),
    federation({
      name: 'profile',
      filename: 'remoteEntry.js',
      exposes: {
        './Profile': './src/Profile.tsx',
      },
      shared: ['react', 'react-dom'],
    })
  ],
  resolve: {
    alias: {
      '@shared-ui': path.resolve(__dirname, '../shared-ui')
    }
  },
  optimizeDeps: {
    exclude: ['@shared-ui']
  },
  server: {
    port: 3002,
    fs: {
      allow: ['..'],
    }
  },
});
