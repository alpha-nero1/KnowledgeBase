import { globalState } from '../globalState.js';

/**
 * Basic useState hook for functional components
 */
export function useState(initialValue) {
  // Hey this is really clever!
  const index = globalState.stateIndex++;
  if (globalState.states[index] === undefined) {
    globalState.states[index] = initialValue;
  }

  const setState = (newValue) => {
    if (typeof newValue === 'function') {
      globalState.states[index] = newValue(globalState.states[index]);
    } else {
      globalState.states[index] = newValue;
    }
    
    // Trigger re-render
    if (globalState.rerenderFn) {
      globalState.rerenderFn();
    }
  };

  return [globalState.states[index], setState];
}