import { globalState, resetGlobalState } from './globalState.js';
import { useState } from './hooks/useState.js';
import { useEffect } from './hooks/useEffect.js';

/**
 * Scaffolds our component into object notation allowing easy processing by the materialise() function.
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
 * 'Materialises' a virtual DOM node to real DOM node.
 * This method is recursive so that it can handle the necessary nested structure of
 * our application.
 */
export const materialise = (virtualNode) => {
  if (typeof virtualNode === 'string') {
    return document.createTextNode(virtualNode);
  }

  if (typeof virtualNode.type === 'function') {
    return materialise(virtualNode.type(virtualNode.props));
  }

  if (virtualNode.type === Fragment) {
    const fragment = document.createDocumentFragment();
    virtualNode.children.forEach(child => {
      const childNode = materialise(child);
      if (childNode) fragment.appendChild(childNode);
    });
    return fragment;
  }

  // Regular element
  const element = document.createElement(virtualNode.type);

  // Props assignment engine:
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

  // Render children!
  virtualNode.children.forEach(child => {
    const childNode = materialise(child);
    if (childNode) element.appendChild(childNode);
  });

  return element;
}

/**
 * Simple app creation and mounting
 */
export const createApp = (entryComponent, container) => {
  const clearContainer = () => { container.innerHTML = ''; };

  const render = () => {
    // Clever! on each rerender pass you need to reset the stateIndex so
    // all of the subsuquent useState are evaluated correctly.
    resetGlobalState();
    const virtualNode = entryComponent();

    // Simple replace strategy (not efficient but simple)
    clearContainer();
    const realNode = materialise(virtualNode);
    container.appendChild(realNode);
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
