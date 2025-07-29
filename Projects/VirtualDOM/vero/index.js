import { globalState } from './globalState.js';
import { useState } from './hooks/useState.js';
import { useEffect } from './hooks/useEffect.js';

/**
 * Creates a virtual DOM element (JSX factory function)
 */
export function vero(type, props, ...children) {
  const flatChildren = children
    .flat(Infinity)
    .filter(child => child != null && child !== false && child !== true)
    .map(child => typeof child === 'string' || typeof child === 'number' ? String(child) : child);

  return {
    type,
    props: props || {},
    children: flatChildren
  };
}

/**
 * Fragment component
 */
export const Fragment = 'FRAGMENT';

/**
 * Renders a virtual DOM node to real DOM
 */
export const render = (virtualNode) => {
  if (typeof virtualNode === 'string') {
    return document.createTextNode(virtualNode);
  }

  if (typeof virtualNode.type === 'function') {
    // Function component
    globalState.stateIndex = 0; // Reset state index for this component
    globalState.effectIndex = 0; // Reset effect index for this component
    return render(virtualNode.type(virtualNode.props));
  }

  if (virtualNode.type === Fragment) {
    const fragment = document.createDocumentFragment();
    virtualNode.children.forEach(child => {
      const childNode = render(child);
      if (childNode) fragment.appendChild(childNode);
    });
    return fragment;
  }

  // Regular element
  const element = document.createElement(virtualNode.type);

  // Set props
  Object.keys(virtualNode.props).forEach(key => {
    if (key === 'onClick') {
      element.addEventListener('click', virtualNode.props[key]);
    } else if (key === 'onChange') {
      element.addEventListener('change', virtualNode.props[key]);
    } else if (key === 'onInput') {
      element.addEventListener('input', virtualNode.props[key]);
    } else if (key === 'className') {
      element.className = virtualNode.props[key];
    } else if (key === 'style' && typeof virtualNode.props[key] === 'object') {
      Object.assign(element.style, virtualNode.props[key]);
    } else if (key.startsWith('on')) {
      const eventName = key.slice(2).toLowerCase();
      element.addEventListener(eventName, virtualNode.props[key]);
    } else {
      element.setAttribute(key, virtualNode.props[key]);
    }
  });

  // Render children
  virtualNode.children.forEach(child => {
    const childNode = render(child);
    if (childNode) element.appendChild(childNode);
  });

  return element;
}

/**
 * Simple app creation and mounting
 */
export const createApp = (entryComponent, container) => {
  let currentNode = null;

  const clearContainer = () => { container.innerHTML = ''; };

  const render = () => {
    // Clever! on each rerender pass you need to reset the stateIndex so
    // all of the subsuquent useState are evaluated correctly.
    globalState.stateIndex = 0;
    globalState.effectIndex = 0;
    const virtualNode = entryComponent();

    // Simple replace strategy (not efficient but simple)
    clearContainer();
    const realNode = render(virtualNode);
    container.appendChild(realNode);
    currentNode = realNode;
  };

  // Set global rerender function
  globalState.rerenderFn = render;

  const unmount = () => {
    clearContainer();
    globalState.rerenderFn = null;
    globalState.states = [];
  }

  return {
    mount: render,
    unmount
  };
}

// Make functions globally available for JSX
if (typeof window !== 'undefined') {
  window.vero = vero;
  window.Fragment = Fragment;
}

// Export hooks and other functions
export { useState, useEffect };
