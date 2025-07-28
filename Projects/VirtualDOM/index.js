import { createApp } from './vero/index.js';
import { App } from './app.js';

// Mount the application.
// NOTE: We listen on DOMContentLoaded and not window.onload because window.onload waits for
// all resources to be loaded like images, scripts and stylesheets.
// for virtual dom you only care about the html being loaded and discoverable, thus DOMContentLoaded is appropriate.
document.addEventListener('DOMContentLoaded', () => {
  const app = createApp(App, document.getElementById('app'));
  app.mount();
});
