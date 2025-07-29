import { vero, useState, useEffect } from './vero/index.js';

// Color Picker Component
export const App = () => {
  const colors = ['#007acc', '#28a745', '#dc3545', '#ffc107', '#6f42c1', '#fd7e14'];

  const [selectedColor, setSelectedColor] = useState(colors[0]);
  const [customColor, setCustomColor] = useState(colors[1]);

  useEffect(() => {
    if (selectedColor === customColor) {
      console.log('Selected color is the same as custom color.');
    }
    return () => {
      console.log('Cleanup function called for color change.');
    }
  }, [selectedColor, customColor]);

  return vero('div', null,
    vero('h4', null, 'Selected Color:'),
    vero('div', {
      className: 'color-box',
      style: { backgroundColor: selectedColor }
    }),
    vero('p', null, `Color: ${selectedColor}`),

    vero('h4', null, 'Pick a color:'),
    vero('div', null,
      ...colors.map(color =>
        vero('div', {
          key: color,
          className: 'color-box',
          style: { backgroundColor: color },
          onClick: () => setSelectedColor(color)
        })
      )
    ),

    vero('h4', null, 'Custom color:'),
    vero('input', {
      type: 'color',
      value: customColor,
      onChange: (e) => setCustomColor(e.target.value)
    }),
    vero('button', {
      className: 'counter-btn',
      onClick: () => setSelectedColor(customColor)
    }, 'Use Custom Color')
  );
}
