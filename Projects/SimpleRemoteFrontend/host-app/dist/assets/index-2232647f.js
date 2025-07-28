import { r as reactExports } from './index-6af61f11.js';
import { r as reactDomExports } from './index-ebe3b9e0.js';

const scriptRel = (function detectScriptRel() {
    const relList = typeof document !== 'undefined' && document.createElement('link').relList;
    return relList && relList.supports && relList.supports('modulepreload')
        ? 'modulepreload'
        : 'preload';
})();const assetsURL = function(dep) { return "/"+dep };const seen = {};const __vitePreload = function preload(baseModule, deps, importerUrl) {
    // @ts-expect-error true will be replaced with boolean later
    if (!true || !deps || deps.length === 0) {
        return baseModule();
    }
    const links = document.getElementsByTagName('link');
    return Promise.all(deps.map((dep) => {
        // @ts-expect-error assetsURL is declared before preload.toString()
        dep = assetsURL(dep);
        if (dep in seen)
            return;
        seen[dep] = true;
        const isCss = dep.endsWith('.css');
        const cssSelector = isCss ? '[rel="stylesheet"]' : '';
        const isBaseRelative = !!importerUrl;
        // check if the file is already preloaded by SSR markup
        if (isBaseRelative) {
            // When isBaseRelative is true then we have `importerUrl` and `dep` is
            // already converted to an absolute URL by the `assetsURL` function
            for (let i = links.length - 1; i >= 0; i--) {
                const link = links[i];
                // The `links[i].href` is an absolute URL thanks to browser doing the work
                // for us. See https://html.spec.whatwg.org/multipage/common-dom-interfaces.html#reflecting-content-attributes-in-idl-attributes:idl-domstring-5
                if (link.href === dep && (!isCss || link.rel === 'stylesheet')) {
                    return;
                }
            }
        }
        else if (document.querySelector(`link[href="${dep}"]${cssSelector}`)) {
            return;
        }
        const link = document.createElement('link');
        link.rel = isCss ? 'stylesheet' : scriptRel;
        if (!isCss) {
            link.as = 'script';
            link.crossOrigin = '';
        }
        link.href = dep;
        document.head.appendChild(link);
        if (isCss) {
            return new Promise((res, rej) => {
                link.addEventListener('load', res);
                link.addEventListener('error', () => rej(new Error(`Unable to preload CSS for ${dep}`)));
            });
        }
    }))
        .then(() => baseModule())
        .catch((err) => {
        const e = new Event('vite:preloadError', { cancelable: true });
        // @ts-expect-error custom payload
        e.payload = err;
        window.dispatchEvent(e);
        if (!e.defaultPrevented) {
            throw err;
        }
    });
};

const buildIdentifier = "[0-9A-Za-z-]+";
const build = `(?:\\+(${buildIdentifier}(?:\\.${buildIdentifier})*))`;
const numericIdentifier = "0|[1-9]\\d*";
const numericIdentifierLoose = "[0-9]+";
const nonNumericIdentifier = "\\d*[a-zA-Z-][a-zA-Z0-9-]*";
const preReleaseIdentifierLoose = `(?:${numericIdentifierLoose}|${nonNumericIdentifier})`;
const preReleaseLoose = `(?:-?(${preReleaseIdentifierLoose}(?:\\.${preReleaseIdentifierLoose})*))`;
const preReleaseIdentifier = `(?:${numericIdentifier}|${nonNumericIdentifier})`;
const preRelease = `(?:-(${preReleaseIdentifier}(?:\\.${preReleaseIdentifier})*))`;
const xRangeIdentifier = `${numericIdentifier}|x|X|\\*`;
const xRangePlain = `[v=\\s]*(${xRangeIdentifier})(?:\\.(${xRangeIdentifier})(?:\\.(${xRangeIdentifier})(?:${preRelease})?${build}?)?)?`;
const hyphenRange = `^\\s*(${xRangePlain})\\s+-\\s+(${xRangePlain})\\s*$`;
const mainVersionLoose = `(${numericIdentifierLoose})\\.(${numericIdentifierLoose})\\.(${numericIdentifierLoose})`;
const loosePlain = `[v=\\s]*${mainVersionLoose}${preReleaseLoose}?${build}?`;
const gtlt = "((?:<|>)?=?)";
const comparatorTrim = `(\\s*)${gtlt}\\s*(${loosePlain}|${xRangePlain})`;
const loneTilde = "(?:~>?)";
const tildeTrim = `(\\s*)${loneTilde}\\s+`;
const loneCaret = "(?:\\^)";
const caretTrim = `(\\s*)${loneCaret}\\s+`;
const star = "(<|>)?=?\\s*\\*";
const caret = `^${loneCaret}${xRangePlain}$`;
const mainVersion = `(${numericIdentifier})\\.(${numericIdentifier})\\.(${numericIdentifier})`;
const fullPlain = `v?${mainVersion}${preRelease}?${build}?`;
const tilde = `^${loneTilde}${xRangePlain}$`;
const xRange = `^${gtlt}\\s*${xRangePlain}$`;
const comparator = `^${gtlt}\\s*(${fullPlain})$|^$`;
const gte0 = "^\\s*>=\\s*0.0.0\\s*$";
function parseRegex(source) {
  return new RegExp(source);
}
function isXVersion(version) {
  return !version || version.toLowerCase() === "x" || version === "*";
}
function pipe(...fns) {
  return (x) => {
    return fns.reduce((v, f) => f(v), x);
  };
}
function extractComparator(comparatorString) {
  return comparatorString.match(parseRegex(comparator));
}
function combineVersion(major, minor, patch, preRelease2) {
  const mainVersion2 = `${major}.${minor}.${patch}`;
  if (preRelease2) {
    return `${mainVersion2}-${preRelease2}`;
  }
  return mainVersion2;
}
function parseHyphen(range) {
  return range.replace(
    parseRegex(hyphenRange),
    (_range, from, fromMajor, fromMinor, fromPatch, _fromPreRelease, _fromBuild, to, toMajor, toMinor, toPatch, toPreRelease) => {
      if (isXVersion(fromMajor)) {
        from = "";
      } else if (isXVersion(fromMinor)) {
        from = `>=${fromMajor}.0.0`;
      } else if (isXVersion(fromPatch)) {
        from = `>=${fromMajor}.${fromMinor}.0`;
      } else {
        from = `>=${from}`;
      }
      if (isXVersion(toMajor)) {
        to = "";
      } else if (isXVersion(toMinor)) {
        to = `<${+toMajor + 1}.0.0-0`;
      } else if (isXVersion(toPatch)) {
        to = `<${toMajor}.${+toMinor + 1}.0-0`;
      } else if (toPreRelease) {
        to = `<=${toMajor}.${toMinor}.${toPatch}-${toPreRelease}`;
      } else {
        to = `<=${to}`;
      }
      return `${from} ${to}`.trim();
    }
  );
}
function parseComparatorTrim(range) {
  return range.replace(parseRegex(comparatorTrim), "$1$2$3");
}
function parseTildeTrim(range) {
  return range.replace(parseRegex(tildeTrim), "$1~");
}
function parseCaretTrim(range) {
  return range.replace(parseRegex(caretTrim), "$1^");
}
function parseCarets(range) {
  return range.trim().split(/\s+/).map((rangeVersion) => {
    return rangeVersion.replace(
      parseRegex(caret),
      (_, major, minor, patch, preRelease2) => {
        if (isXVersion(major)) {
          return "";
        } else if (isXVersion(minor)) {
          return `>=${major}.0.0 <${+major + 1}.0.0-0`;
        } else if (isXVersion(patch)) {
          if (major === "0") {
            return `>=${major}.${minor}.0 <${major}.${+minor + 1}.0-0`;
          } else {
            return `>=${major}.${minor}.0 <${+major + 1}.0.0-0`;
          }
        } else if (preRelease2) {
          if (major === "0") {
            if (minor === "0") {
              return `>=${major}.${minor}.${patch}-${preRelease2} <${major}.${minor}.${+patch + 1}-0`;
            } else {
              return `>=${major}.${minor}.${patch}-${preRelease2} <${major}.${+minor + 1}.0-0`;
            }
          } else {
            return `>=${major}.${minor}.${patch}-${preRelease2} <${+major + 1}.0.0-0`;
          }
        } else {
          if (major === "0") {
            if (minor === "0") {
              return `>=${major}.${minor}.${patch} <${major}.${minor}.${+patch + 1}-0`;
            } else {
              return `>=${major}.${minor}.${patch} <${major}.${+minor + 1}.0-0`;
            }
          }
          return `>=${major}.${minor}.${patch} <${+major + 1}.0.0-0`;
        }
      }
    );
  }).join(" ");
}
function parseTildes(range) {
  return range.trim().split(/\s+/).map((rangeVersion) => {
    return rangeVersion.replace(
      parseRegex(tilde),
      (_, major, minor, patch, preRelease2) => {
        if (isXVersion(major)) {
          return "";
        } else if (isXVersion(minor)) {
          return `>=${major}.0.0 <${+major + 1}.0.0-0`;
        } else if (isXVersion(patch)) {
          return `>=${major}.${minor}.0 <${major}.${+minor + 1}.0-0`;
        } else if (preRelease2) {
          return `>=${major}.${minor}.${patch}-${preRelease2} <${major}.${+minor + 1}.0-0`;
        }
        return `>=${major}.${minor}.${patch} <${major}.${+minor + 1}.0-0`;
      }
    );
  }).join(" ");
}
function parseXRanges(range) {
  return range.split(/\s+/).map((rangeVersion) => {
    return rangeVersion.trim().replace(
      parseRegex(xRange),
      (ret, gtlt2, major, minor, patch, preRelease2) => {
        const isXMajor = isXVersion(major);
        const isXMinor = isXMajor || isXVersion(minor);
        const isXPatch = isXMinor || isXVersion(patch);
        if (gtlt2 === "=" && isXPatch) {
          gtlt2 = "";
        }
        preRelease2 = "";
        if (isXMajor) {
          if (gtlt2 === ">" || gtlt2 === "<") {
            return "<0.0.0-0";
          } else {
            return "*";
          }
        } else if (gtlt2 && isXPatch) {
          if (isXMinor) {
            minor = 0;
          }
          patch = 0;
          if (gtlt2 === ">") {
            gtlt2 = ">=";
            if (isXMinor) {
              major = +major + 1;
              minor = 0;
              patch = 0;
            } else {
              minor = +minor + 1;
              patch = 0;
            }
          } else if (gtlt2 === "<=") {
            gtlt2 = "<";
            if (isXMinor) {
              major = +major + 1;
            } else {
              minor = +minor + 1;
            }
          }
          if (gtlt2 === "<") {
            preRelease2 = "-0";
          }
          return `${gtlt2 + major}.${minor}.${patch}${preRelease2}`;
        } else if (isXMinor) {
          return `>=${major}.0.0${preRelease2} <${+major + 1}.0.0-0`;
        } else if (isXPatch) {
          return `>=${major}.${minor}.0${preRelease2} <${major}.${+minor + 1}.0-0`;
        }
        return ret;
      }
    );
  }).join(" ");
}
function parseStar(range) {
  return range.trim().replace(parseRegex(star), "");
}
function parseGTE0(comparatorString) {
  return comparatorString.trim().replace(parseRegex(gte0), "");
}
function compareAtom(rangeAtom, versionAtom) {
  rangeAtom = +rangeAtom || rangeAtom;
  versionAtom = +versionAtom || versionAtom;
  if (rangeAtom > versionAtom) {
    return 1;
  }
  if (rangeAtom === versionAtom) {
    return 0;
  }
  return -1;
}
function comparePreRelease(rangeAtom, versionAtom) {
  const { preRelease: rangePreRelease } = rangeAtom;
  const { preRelease: versionPreRelease } = versionAtom;
  if (rangePreRelease === void 0 && !!versionPreRelease) {
    return 1;
  }
  if (!!rangePreRelease && versionPreRelease === void 0) {
    return -1;
  }
  if (rangePreRelease === void 0 && versionPreRelease === void 0) {
    return 0;
  }
  for (let i = 0, n = rangePreRelease.length; i <= n; i++) {
    const rangeElement = rangePreRelease[i];
    const versionElement = versionPreRelease[i];
    if (rangeElement === versionElement) {
      continue;
    }
    if (rangeElement === void 0 && versionElement === void 0) {
      return 0;
    }
    if (!rangeElement) {
      return 1;
    }
    if (!versionElement) {
      return -1;
    }
    return compareAtom(rangeElement, versionElement);
  }
  return 0;
}
function compareVersion(rangeAtom, versionAtom) {
  return compareAtom(rangeAtom.major, versionAtom.major) || compareAtom(rangeAtom.minor, versionAtom.minor) || compareAtom(rangeAtom.patch, versionAtom.patch) || comparePreRelease(rangeAtom, versionAtom);
}
function eq(rangeAtom, versionAtom) {
  return rangeAtom.version === versionAtom.version;
}
function compare(rangeAtom, versionAtom) {
  switch (rangeAtom.operator) {
    case "":
    case "=":
      return eq(rangeAtom, versionAtom);
    case ">":
      return compareVersion(rangeAtom, versionAtom) < 0;
    case ">=":
      return eq(rangeAtom, versionAtom) || compareVersion(rangeAtom, versionAtom) < 0;
    case "<":
      return compareVersion(rangeAtom, versionAtom) > 0;
    case "<=":
      return eq(rangeAtom, versionAtom) || compareVersion(rangeAtom, versionAtom) > 0;
    case void 0: {
      return true;
    }
    default:
      return false;
  }
}
function parseComparatorString(range) {
  return pipe(
    parseCarets,
    parseTildes,
    parseXRanges,
    parseStar
  )(range);
}
function parseRange(range) {
  return pipe(
    parseHyphen,
    parseComparatorTrim,
    parseTildeTrim,
    parseCaretTrim
  )(range.trim()).split(/\s+/).join(" ");
}
function satisfy(version, range) {
  if (!version) {
    return false;
  }
  const parsedRange = parseRange(range);
  const parsedComparator = parsedRange.split(" ").map((rangeVersion) => parseComparatorString(rangeVersion)).join(" ");
  const comparators = parsedComparator.split(/\s+/).map((comparator2) => parseGTE0(comparator2));
  const extractedVersion = extractComparator(version);
  if (!extractedVersion) {
    return false;
  }
  const [
    ,
    versionOperator,
    ,
    versionMajor,
    versionMinor,
    versionPatch,
    versionPreRelease
  ] = extractedVersion;
  const versionAtom = {
    operator: versionOperator,
    version: combineVersion(
      versionMajor,
      versionMinor,
      versionPatch,
      versionPreRelease
    ),
    major: versionMajor,
    minor: versionMinor,
    patch: versionPatch,
    preRelease: versionPreRelease == null ? void 0 : versionPreRelease.split(".")
  };
  for (const comparator2 of comparators) {
    const extractedComparator = extractComparator(comparator2);
    if (!extractedComparator) {
      return false;
    }
    const [
      ,
      rangeOperator,
      ,
      rangeMajor,
      rangeMinor,
      rangePatch,
      rangePreRelease
    ] = extractedComparator;
    const rangeAtom = {
      operator: rangeOperator,
      version: combineVersion(
        rangeMajor,
        rangeMinor,
        rangePatch,
        rangePreRelease
      ),
      major: rangeMajor,
      minor: rangeMinor,
      patch: rangePatch,
      preRelease: rangePreRelease == null ? void 0 : rangePreRelease.split(".")
    };
    if (!compare(rangeAtom, versionAtom)) {
      return false;
    }
  }
  return true;
}

const currentImports$1 = {};

// eslint-disable-next-line no-undef
const moduleMap = {'react':{get:()=>()=>__federation_import$1(new URL('__federation_shared_react-57f88920.js', import.meta.url).href),import:true},'react-dom':{get:()=>()=>__federation_import$1(new URL('__federation_shared_react-dom-a7434114.js', import.meta.url).href),import:true}};
const moduleCache = Object.create(null);
async function importShared(name, shareScope = 'default') {
  return moduleCache[name]
    ? new Promise((r) => r(moduleCache[name]))
    : (await getSharedFromRuntime(name, shareScope)) || getSharedFromLocal(name)
}
// eslint-disable-next-line
async function __federation_import$1(name) {
  currentImports$1[name] ??= __vitePreload(() => import(name),true?[]:void 0);
  return currentImports$1[name]
}
async function getSharedFromRuntime(name, shareScope) {
  let module = null;
  if (globalThis?.__federation_shared__?.[shareScope]?.[name]) {
    const versionObj = globalThis.__federation_shared__[shareScope][name];
    const requiredVersion = moduleMap[name]?.requiredVersion;
    const hasRequiredVersion = !!requiredVersion;
    if (hasRequiredVersion) {
      const versionKey = Object.keys(versionObj).find((version) =>
        satisfy(version, requiredVersion)
      );
      if (versionKey) {
        const versionValue = versionObj[versionKey];
        module = await (await versionValue.get())();
      } else {
        console.log(
          `provider support ${name}(${versionKey}) is not satisfied requiredVersion(\${moduleMap[name].requiredVersion})`
        );
      }
    } else {
      const versionKey = Object.keys(versionObj)[0];
      const versionValue = versionObj[versionKey];
      module = await (await versionValue.get())();
    }
  }
  if (module) {
    return flattenModule(module, name)
  }
}
async function getSharedFromLocal(name) {
  if (moduleMap[name]?.import) {
    let module = await (await moduleMap[name].get())();
    return flattenModule(module, name)
  } else {
    console.error(
      `consumer config import=false,so cant use callback shared module`
    );
  }
}
function flattenModule(module, name) {
  // use a shared module which export default a function will getting error 'TypeError: xxx is not a function'
  if (typeof module.default === 'function') {
    Object.keys(module).forEach((key) => {
      if (key !== 'default') {
        module.default[key] = module[key];
      }
    });
    moduleCache[name] = module.default;
    return module.default
  }
  if (module.default) module = Object.assign({}, module.default, module);
  moduleCache[name] = module;
  return module
}

var jsxRuntime = {exports: {}};

var reactJsxRuntime_production_min = {};

/**
 * @license React
 * react-jsx-runtime.production.min.js
 *
 * Copyright (c) Facebook, Inc. and its affiliates.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */
var f=reactExports,k=Symbol.for("react.element"),l=Symbol.for("react.fragment"),m$1=Object.prototype.hasOwnProperty,n=f.__SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED.ReactCurrentOwner,p={key:!0,ref:!0,__self:!0,__source:!0};
function q(c,a,g){var b,d={},e=null,h=null;void 0!==g&&(e=""+g);void 0!==a.key&&(e=""+a.key);void 0!==a.ref&&(h=a.ref);for(b in a)m$1.call(a,b)&&!p.hasOwnProperty(b)&&(d[b]=a[b]);if(c&&c.defaultProps)for(b in a=c.defaultProps,a)void 0===d[b]&&(d[b]=a[b]);return {$$typeof:k,type:c,key:e,ref:h,props:d,_owner:n.current}}reactJsxRuntime_production_min.Fragment=l;reactJsxRuntime_production_min.jsx=q;reactJsxRuntime_production_min.jsxs=q;

{
  jsxRuntime.exports = reactJsxRuntime_production_min;
}

var jsxRuntimeExports = jsxRuntime.exports;

var client = {};

var m = reactDomExports;
{
  client.createRoot = m.createRoot;
  client.hydrateRoot = m.hydrateRoot;
}

const remotesMap = {
'remoteApp':{url:'http://localhost:4001/assets/remoteEntry.js',format:'esm',from:'vite'}
};
                const currentImports = {};
                const loadJS = async (url, fn) => {
                    const resolvedUrl = typeof url === 'function' ? await url() : url;
                    const script = document.createElement('script');
                    script.type = 'text/javascript';
                    script.onload = fn;
                    script.src = resolvedUrl;
                    document.getElementsByTagName('head')[0].appendChild(script);
                };

                function get(name, remoteFrom) {
                    return __federation_import(name).then(module => () => {
                        if (remoteFrom === 'webpack') {
                            return Object.prototype.toString.call(module).indexOf('Module') > -1 && module.default ? module.default : module
                        }
                        return module
                    })
                }
                
                function merge(obj1, obj2) {
                  const mergedObj = Object.assign(obj1, obj2);
                  for (const key of Object.keys(mergedObj)) {
                    if (typeof mergedObj[key] === 'object' && typeof obj2[key] === 'object') {
                      mergedObj[key] = merge(mergedObj[key], obj2[key]);
                    }
                  }
                  return mergedObj;
                }

                const wrapShareModule = remoteFrom => {
                  return merge({
                    'react':{'18.3.1':{get:()=>get(new URL('__federation_shared_react-57f88920.js', import.meta.url).href, remoteFrom), loaded:1}},'react-dom':{'18.3.1':{get:()=>get(new URL('__federation_shared_react-dom-a7434114.js', import.meta.url).href, remoteFrom), loaded:1}}
                  }, (globalThis.__federation_shared__ || {})['default'] || {});
                };

                async function __federation_import(name) {
                    currentImports[name] ??= __vitePreload(() => import(name),true?[]:void 0);
                    return currentImports[name]
                }

                async function __federation_method_ensure(remoteId) {
                    const remote = remotesMap[remoteId];
                    if (!remote.inited) {
                        if ('var' === remote.format) {
                            // loading js with script tag
                            return new Promise(resolve => {
                                const callback = () => {
                                    if (!remote.inited) {
                                        remote.lib = window[remoteId];
                                        remote.lib.init(wrapShareModule(remote.from));
                                        remote.inited = true;
                                    }
                                    resolve(remote.lib);
                                };
                                return loadJS(remote.url, callback);
                            });
                        } else if (['esm', 'systemjs'].includes(remote.format)) {
                            // loading js with import(...)
                            return new Promise((resolve, reject) => {
                                const getUrl = typeof remote.url === 'function' ? remote.url : () => Promise.resolve(remote.url);
                                getUrl().then(url => {
                                    __vitePreload(() => import(/* @vite-ignore */ url),true?[]:void 0).then(lib => {
                                        if (!remote.inited) {
                                            const shareScope = wrapShareModule(remote.from);
                                            lib.init(shareScope);
                                            remote.lib = lib;
                                            remote.lib.init(shareScope);
                                            remote.inited = true;
                                        }
                                        resolve(remote.lib);
                                    }).catch(reject);
                                });
                            })
                        }
                    } else {
                        return remote.lib;
                    }
                }

                function __federation_method_wrapDefault(module, need) {
                    if (!module?.default && need) {
                        let obj = Object.create(null);
                        obj.default = module;
                        obj.__esModule = true;
                        return obj;
                    }
                    return module;
                }

                function __federation_method_getRemote(remoteName, componentName) {
                    return __federation_method_ensure(remoteName).then((remote) => remote.get(componentName).then(factory => factory()));
                }

const React$1 = await importShared('react');
const {Suspense} = React$1;

const RemoteButton = React$1.lazy(() => __federation_method_getRemote("remoteApp" , "./Button").then(module=>__federation_method_wrapDefault(module, true)));
const RemoteCounter = React$1.lazy(() => __federation_method_getRemote("remoteApp" , "./Counter").then(module=>__federation_method_wrapDefault(module, true)));
function App() {
  return /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: { padding: "20px", maxWidth: "800px", margin: "0 auto" }, children: [
    /* @__PURE__ */ jsxRuntimeExports.jsx("h1", { children: "Host Application" }),
    /* @__PURE__ */ jsxRuntimeExports.jsx("p", { children: "This is the main host application that consumes microfrontends." }),
    /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: {
      border: "2px solid #9C27B0",
      padding: "20px",
      borderRadius: "8px",
      marginBottom: "20px",
      backgroundColor: "#f8f9fa"
    }, children: [
      /* @__PURE__ */ jsxRuntimeExports.jsx("h2", { children: "Local Host Content" }),
      /* @__PURE__ */ jsxRuntimeExports.jsx("p", { children: "This content is rendered by the host application itself." }),
      /* @__PURE__ */ jsxRuntimeExports.jsx("button", { style: {
        backgroundColor: "#9C27B0",
        color: "white",
        padding: "10px 20px",
        border: "none",
        borderRadius: "4px",
        cursor: "pointer"
      }, children: "Host Button" })
    ] }),
    /* @__PURE__ */ jsxRuntimeExports.jsxs("div", { style: {
      border: "2px solid #FF9800",
      padding: "20px",
      borderRadius: "8px",
      backgroundColor: "#fff3e0"
    }, children: [
      /* @__PURE__ */ jsxRuntimeExports.jsx("h2", { children: "Remote Microfrontend Components" }),
      /* @__PURE__ */ jsxRuntimeExports.jsx("p", { children: "The components below are loaded from the remote microfrontend:" }),
      /* @__PURE__ */ jsxRuntimeExports.jsx(Suspense, { fallback: /* @__PURE__ */ jsxRuntimeExports.jsx("div", { children: "Loading Remote Button..." }), children: /* @__PURE__ */ jsxRuntimeExports.jsx(RemoteButton, {}) }),
      /* @__PURE__ */ jsxRuntimeExports.jsx(Suspense, { fallback: /* @__PURE__ */ jsxRuntimeExports.jsx("div", { children: "Loading Remote Counter..." }), children: /* @__PURE__ */ jsxRuntimeExports.jsx(RemoteCounter, {}) })
    ] })
  ] });
}

const index = '';

const React = await importShared('react');
client.createRoot(document.getElementById("root")).render(
  /* @__PURE__ */ jsxRuntimeExports.jsx(React.StrictMode, { children: /* @__PURE__ */ jsxRuntimeExports.jsx(App, {}) })
);
