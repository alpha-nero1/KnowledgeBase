export const globalState = {
  states: [],
  stateIndex: 0,
  // Each useState has access to the rerenderFn to refresh the page
  // after state change.
  rerenderFn: null,
  // useEffectState
  effectDependencies: [],
  effectIndex: 0,
  effectCleanupFns: []
};
