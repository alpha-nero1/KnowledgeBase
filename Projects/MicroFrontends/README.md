# 🚟 Projects / MicroFrontends
> [!IMPORTANT]
> This code demonstrates best practices and is very cool.

## 🤷‍♂️ What does it do?
This project demonstrates key features of an effective microfrontend setup. These include:
- ✅ Host app that loads remote apps (micro frontends)
- ✅ Host app employs standard structure & **routing** to the overall application.
- ✅ All apps share a common `shared-ui` component library.

## 🛠️ Project setup
```
MicroFrontends/
├── host-app/           # Main application (consumer) - port 3000
└── dashboard/          # Microfrontend (provider) - port 3001
└── profile/            # Microfrontend (provider) - port 3002
```

## 🏎️ How to run 
1. Run `npm run i:all` in order to install all dependencies.
2. Run `npm run d:all` in order to run all 3 dev servers (host, dashboard & profile)

Now when you visit `http://localhost:3000` you will see the host app rendered with the sub modules loaded in.


## 👷 How It Works
1. The **remote app** uses `@module-federation/vite` to expose components
2. The **host app** imports these components dynamically
3. Both apps share React dependencies to avoid duplication
4. Components are loaded on-demand using React Suspense

## ⚖️ Final Remarks
This is a fantastic and simple example of how to set up a comprehensive microfrontend architecture. This can be a blessing if you have multiple teams that are developing the one product.

It means that each team can focus on developing their own micro front end and the host app just needs to stitch them together. This development independence allows many hands to make quick work of a large cross-team initiative.