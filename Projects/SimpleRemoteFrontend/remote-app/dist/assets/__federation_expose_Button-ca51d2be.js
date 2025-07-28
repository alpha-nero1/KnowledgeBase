import { importShared } from './__federation_fn_import-e40783d0.js';
import { j as jsxRuntimeExports } from './jsx-runtime-5c117c81.js';

await importShared('react');

const Button = () => {
  const handleClick = () => {
    alert("Button clicked from Remote App!");
  };
  return /* @__PURE__ */ jsxRuntimeExports.jsx(
    "button",
    {
      onClick: handleClick,
      style: {
        backgroundColor: "blue",
        color: "white",
        padding: "10px 20px",
        border: "none",
        borderRadius: "4px",
        cursor: "pointer",
        margin: "5px"
      },
      children: "Remote Button"
    }
  );
};

export { Button as default };
