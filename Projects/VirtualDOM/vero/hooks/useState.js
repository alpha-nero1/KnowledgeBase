import { globalState } from '../globalState.js';

/**
 * State hook implementation!
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
    if (typeof globalState.rerenderFn !== 'function') {
      throw new Error('Rerender function is not set. Ensure that it is and that this hook is used within a component context');
    }

    globalState.rerenderFn();
  };

  return [globalState.states[index], setState];
}