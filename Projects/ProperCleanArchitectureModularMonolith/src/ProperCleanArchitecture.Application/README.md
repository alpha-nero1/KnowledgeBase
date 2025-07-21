# 📱 Application
Business logic and contracts.

## 🔑 Key considerations
### Treat each feature folder like a standalone app.
This is core to modular monolith. Each app folder should be self contained with NO intercrossing logic. If you need to get something from another module make sure you do so using mediatior (and that the mediator handler returns a result from a shared contract)

This facilitates both:
1) Separation of concerns - you can be more confident that your change in one sub app will not effect another.
2) Position yourself for potential microservicification - If you want to upgrade to microservices you only need to replace your mediator calls with API calls!


### Be wary of the /Shared folder
There is a Shared folder here for Shared/Common logic across microservices. When this app gets larger/on the verge of microservisification; consider turning this into a classlib that all the modules/services can use.