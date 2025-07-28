import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import { federation } from '@module-federation/vite';
// import path from 'path';

export default defineConfig({
  plugins: [
    react(),
    federation({
      name: 'profileApp',
      filename: 'remoteEntry.js',
      exposes: {
        './App': './src/App.tsx',
      },
      shared: {
        react: { 
          singleton: true,
          requiredVersion: '^18.0.0'
        },
        'react-dom': { 
          singleton: true,
          requiredVersion: '^18.0.0'
        }
      }
    })
  ],
  // resolve: {
  //   alias: {
  //     '@shared-ui': path.resolve(__dirname, '../shared-ui')
  //   }
  // },
  // server: {
  //   port: 3002,
  //   fs: {
  //     allow: ['..'],
  //   }
  // },
  build: {
    modulePreload: false,
    target: 'esnext',
    minify: false,
    cssCodeSplit: false
  }
});
