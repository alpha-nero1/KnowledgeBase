import { importShared } from './__federation_fn_import-e40783d0.js';
import { j as jsxRuntimeExports } from './jsx-runtime-5c117c81.js';
import { r as reactDomExports } from './index-ebe3b9e0.js';
import Button from './__federation_expose_Button-ca51d2be.js';
import Counter from './__federation_expose_Counter-2626d1de.js';

var client = {};

var m = reactDomExports;
{
  client.createRoot = m.createRoot;
  client.hydrateRoot = m.hydrateRoot;
}

await importShared('react');
function App() {
  return /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: { padding: "20px" }, children: [
    /* @__PURE__ */ jsxRuntimeExports.jsx("h1", { children: "Remote App" }),
    /* @__PURE__ */ jsxRuntimeExports.jsx("p", { children: "This is the remote microfrontend application." }),
    /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: { marginBottom: "20px" }, children: [
      /* @__PURE__ */ jsxRuntimeExports.jsx("h3", { children: "Exposed Components:" }),
      /* @__PURE__ */ jsxRuntimeExports.jsx(Button, {}),
      /* @__PURE__ */ jsxRuntimeExports.jsx(Counter, {})
    ] })
  ] });
}

const index = '';

const React = await importShared('react');
client.createRoot(document.getElementById("root")).render(
  /* @__PURE__ */ jsxRuntimeExports.jsx(React.StrictMode, { children: /* @__PURE__ */ jsxRuntimeExports.jsx(App, {}) })
);
