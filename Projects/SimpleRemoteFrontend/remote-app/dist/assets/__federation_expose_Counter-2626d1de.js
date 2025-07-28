import { importShared } from './__federation_fn_import-e40783d0.js';
import { j as jsxRuntimeExports } from './jsx-runtime-5c117c81.js';

const React = await importShared('react');
const {useState} = React;

const Counter = () => {
  const [count, setCount] = useState(0);
  return /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: {
    border: "2px solid #2196F3",
    padding: "15px",
    borderRadius: "8px",
    margin: "10px 0",
    textAlign: "center"
  }, children: [
    /* @__PURE__ */ jsxRuntimeExports.jsx("h3", { children: "Remote Counter Component" }),
    /* @__PURE__ */ jsxRuntimeExports.jsxs("p", { children: [
      "Count: ",
      count
    ] }),
    /* @__PURE__ */ jsxRuntimeExports.jsx(
      "button",
      {
        onClick: () => setCount(count + 1),
        style: {
          backgroundColor: "#2196F3",
          color: "white",
          padding: "8px 16px",
          border: "none",
          borderRadius: "4px",
          cursor: "pointer",
          margin: "0 5px"
        },
        children: "+"
      }
    ),
    /* @__PURE__ */ jsxRuntimeExports.jsx(
      "button",
      {
        onClick: () => setCount(count - 1),
        style: {
          backgroundColor: "#f44336",
          color: "white",
          padding: "8px 16px",
          border: "none",
          borderRadius: "4px",
          cursor: "pointer",
          margin: "0 5px"
        },
        children: "-"
      }
    )
  ] });
};

export { Counter as default };
