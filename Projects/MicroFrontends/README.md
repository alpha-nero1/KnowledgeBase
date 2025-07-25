# Microfrontends Demo with Module Federation

This project demonstrates a best-practice microfrontends architecture using React, Vite, and Webpack 5 Module Federation.

## Structure
- **Host** (main app, port 5000)
- **Dashboard** (microfrontend, port 5001)
- **Profile** (microfrontend, port 5002)
- **Shared-UI** (shared component library, port 5003)

## How to Run
Start each app in a separate terminal:

```bash
# 1. Start shared-ui
cd shared-ui && npm run dev

# 2. Start dashboard
cd dashboard && npm run dev

# 3. Start profile
cd profile && npm run dev

# 4. Start host (main folder)
npm run dev
```

Open [http://localhost:5000](http://localhost:5000) to view the host app with integrated microfrontends.

## Features
- Each microfrontend is independently developed and deployed
- Shared UI components via module federation
- TypeScript, Vite, React, and best practices

## Scripts
You can add npm scripts or use tools like concurrently for easier startup.

---
For more details, see each subfolder's README or source code.
