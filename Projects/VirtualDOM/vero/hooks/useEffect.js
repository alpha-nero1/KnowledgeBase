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

export const useEffect = (effect, deps = []) => {
    const currentIndex = globalState.effectIndex++;

    // if dependencies have not changed, return.
    const existingDeps = globalState.effectDependencies[currentIndex]
    if (equals(existingDeps, deps)) return;

    // Re-assign new dependencies.
    globalState.effectDependencies[currentIndex] = deps;
}