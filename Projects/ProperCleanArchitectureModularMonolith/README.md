# 🧠 Projects / ProperCleanArchitectureModularMonolith
> ℹ️ This repository does reflect best practices.

## 🤷‍♂️ What does it do?
This project is an ideal model of what a CleanArchitecture Modular Monolith project should look like.

### 🌳 Dependency tree structure
┌─────────────────────────────────────────────────────────────┐
│                    ProperCleanArchitecture.Api             │
│                         (Presentation)                     │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│            ProperCleanArchitecture.Infrastructure          │
│                      (External)                            │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│            ProperCleanArchitecture.Application             │
│                     (Use Cases)                            │
└─────────────────────┬───────────────────────────────────────┘
                      │
        ┌─────────────▼─────────────┐    ┌──────────────────────┐
        │ ProperCleanArchitecture   │    │ ProperCleanArch      │
        │        .Domain            │    │   .Contracts         │
        │     (Entities/Rules)      │    │  (Shared DTOs)       │
        └───────────────────────────┘    └──────────────────────┘

### ⚡ Rules / Things to look out for
#### ❌ No cross-module repository or service usage!
This is a modular monolith project so be careful to avoid cross-module repository usage. This is an antipattern. If you need data from another module or want to save data to another module, add and use a new mediator query/command to do so. This means that in the event of going from Modular Monolith to full blown Microservices you only need to replace your mediator calls with API calls.

#### 📦 Feature folders are almost self contained apps
Each feature folder in .Application contains only logic that pertains to it, and thus we avoid (for the most part) central locations where we define cross-feature logic. This means that each feature folder has its own folders; `Commands`, `Queries`, `DTOs` (specific to the feature), `Interfaces`, `Services`, `Profiles`, `Validators`.

#### ℹ️ Importance of the infrastructure layer
`Infrastructure` is responsible for any and all outside integration, including to your own DB!

#### 👷 Split of real code and test code
The source code is split into `src` and `tests` but part of the same solution, this helps separate real code and test code.

#### What the helly is .Contracts
The contracts project allows your modules to share DTOs/contracts in order to facilitate ease of communication between modules (you now have cleat API contracts between modules!).
Adding to this if your contracts change, you can version them! by adding `ProperCleanArchitecture.Contracts.V2` and so on.

## ⚖️ Final Remarks
This project is great because it demonstrates best practice for enterprise systems, ensuring that the product you are working on assumes the least amount of technical debt possible during development and allows extensibility into the microservice world.

To be honest my favourite part of this set up though is having multiple modules (can eventually make into microservices) but it is all in the one solution, meaning having a shared Domain and logic is soooooo easy!


## Shutdown
- Add order placement.
* Later you can make this into a template and use for the document procesing project.