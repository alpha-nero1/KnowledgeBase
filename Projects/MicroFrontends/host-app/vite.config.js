import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { federation } from '@module-federation/vite';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    federation({
      name: 'host_app',
      remotes: {
        // THIS is the magic right here, we need to specify type: module
        dashboardApp: {
					type: 'module',
					name: 'dashboardApp',
					entry: 'http://localhost:3001/remoteEntry.js',
					entryGlobalName: 'dashboardApp',
					shareScope: 'default'
				},
        profileApp: {
					type: 'module',
					name: 'profileApp',
					entry: 'http://localhost:3002/remoteEntry.js',
					entryGlobalName: 'profileApp',
					shareScope: 'default'
				}
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
  build: {
    modulePreload: false,
    target: 'esnext',
    minify: false,
    cssCodeSplit: false
  }
})
