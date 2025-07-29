import { globalState } from '../globalState.js';

const equals = (arrayOne = [], arrayTwo = []) => {
    if (arrayOne.length !== arrayTwo.length) return false;
    return arrayOne.every((item, index) => {
        if (Array.isArray(item) && Array.isArray(arrayTwo[index])) {
            return equals(item, arrayTwo[index]);
        }
        return item === arrayTwo[index];
    });
}

/**
 * Basic useEffect hook for functional components.
 */
export const useEffect = (effect, deps = []) => {
    const currentIndex = globalState.effectIndex++;

    // if dependencies have not changed, return.
    const existingDeps = globalState.effectDependencies[currentIndex]
    if (equals(existingDeps, deps)) return;

    // If there is a cleanup func for this index, run it!
    if (typeof globalState.effectCleanupFns[currentIndex] === 'function') {
        globalState.effectCleanupFns[currentIndex]();
    }

    // Re-assign new dependencies.
    globalState.effectDependencies[currentIndex] = deps;
    const result = effect();
    if (typeof result === 'function') {
        globalState.effectCleanupFns[currentIndex] = result;
    }
}