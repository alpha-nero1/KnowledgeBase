# 📦 Projects / VirtualDOM (vero)
> [!WARNING]
> *The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
In this project we have created our own VirtualDOM framework!! It is basic but works really well.
See the `vero` subfloder to uncover how it all works! Specifically we covered:
- ✔️ How to create a VirtualDOM framework.
- ✔️ How to bundle the application up into a single `index.js` ready for static FE consumption.
- ✔️ How to handle hooks and true responsiveness withing the framework.

## 👷 How does it work?
In essence this works because we:
1. Create a virtual representation of our DOM using the `vero` scaffolding.
2. We pass that virtualDOM into a `materialise()` method which recursively scans the VirtualDOM tree from the root node and renders real document elements.
3. We use a system of hooks to trigger re-renders inside the application.

Have an explore of the `./vero` folder to get a good grasp on the inner workings, it's pretty neat! I'm gonna put react out of business lol.


## 🛠️ Project setup
`package.json` - dependencies for vite.
`index.js` - FE app entry point (mounts the app onto an element)
`app.js` - Our main implementation leveraging `vero`
`index.html` - Entry page for the application.
`./vero` - Custom VirtualDOM framework called `vero`
`./dist` - Minified bundled code that `index.html` uses.

## 🏎️ How to run 
1. Run `npm watch` to kick off the watched buikd process.
2. Now open the index.html in your browser, consequent code changes you make will be shown there!

Enjoy!

## ⚖️ Final Remarks
VirtualDOM frameworks are powerfull tools and it is important to understand how they actually work, this project does just that by creating a framework called `vero` to which the `index.js` in this project consumes to show a completely responsive colour picker application.

**Stay classy San Diego ~ Ale**