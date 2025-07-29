/**
 * Tracl the global render state. Important for hooks and reactivity!
 */
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

/**
 * On each render pass we need to reset these key values so that when all the
 * hooks run again, they get the correct values from their index slot.
 */
export const resetGlobalState = () => {
    globalState.stateIndex = 0;
    globalState.effectIndex = 0;
}
