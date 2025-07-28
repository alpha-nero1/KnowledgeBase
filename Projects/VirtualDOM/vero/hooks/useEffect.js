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
    const newIndex = globalState.effectIndex++;

    // if dependencies have not changed, return.
    const existingDeps = globalState.effectDependencies[newIndex]
    if (equals(existingDeps, deps)) return;
    
    globalState.effectDependencies[newIndex] = deps;
}