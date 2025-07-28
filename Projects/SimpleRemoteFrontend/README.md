# 🧰 Projects / SimpleRemoteFrontend <name>
> [!WARNING]
> *The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
This project demonstrates how a minimal example of microfrontend architecture works. This project levergaes a key feature of vite in order to facilitate the microfrontend architecture, that is; module federaton.

## 🛠️ Project setup
```
SimpleRemoteFrontend/
├── host-app/          # Main application (consumer)
└── remote-app/        # Microfrontend (provider)
```

- **Host App** (Port 4000): The main application that consumes microfrontends
- **Remote App** (Port 4001): A microfrontend that exposes reusable components

The remote app exposes two components:
- `Button`: A simple button component
- `Counter`: A stateful counter component

## 🏎️ How to run 
1. Run `npm run install:all` in order to install all dependencies.
2. `cd remote-app` and run `npm run build:watch`. In parrallel run `npm run preview` this will ensure your remote-app keeps building the remoteEntry.js on code changes.
3. In a new terminal run `npm run start:host` to kick off the host app.

Now when you visit `http://localhost:4000` you will see the host app rendered with the sub modules loaded in.


## 👷 How It Works
1. The **remote app** uses `@originjs/vite-plugin-federation` to expose components
2. The **host app** imports these components dynamically
3. Both apps share React dependencies to avoid duplication
4. Components are loaded on-demand using React Suspense

## ⚖️ Final Remarks
This is a fantastic and simple example of how to set up microfrontend architecture. This can be a blessing if you have multiple teams that are developing the one product.

It means that each team can focus on developing their own micro front end and the host app just needs to stitch them together. This development independence allows many hands to make quick work of a large cross-team initiative.

## Caveats
> [!WARNING]
> The remote makes use of `vite build --watch` for it's dev server, this is how we are able to detect code changes in the remote and automatically rebuild and serve the necessary `remoteEntry.js`. For some reason running `vite` (dev server) does not produce this file for us (even though the net says it should). In the perfect world we would want this to work so that the host app does not need to refresh the page to see the remote app code changes.