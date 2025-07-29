import { vero, useState, useEffect } from './vero/index.js';

// Color Picker Component
export const App = () => {
  const colors = ['#007acc', '#28a745', '#dc3545', '#ffc107', '#6f42c1', '#fd7e14'];

  const [selectedColor, setSelectedColor] = useState(colors[0]);
  const [customColor, setCustomColor] = useState(colors[1]);

  useEffect(() => {
    // Logging demonstrates that use effect is working!
    console.log('Color changed:', selectedColor, customColor);
    return () => {
      console.log('Cleanup function called for color change.');
    }
  }, [selectedColor, customColor]);

  return vero('div', { id: 'color-app' },
    vero('h4', null, 'Selected Color:'),
    vero('div', {
      className: 'color-box',
      style: { backgroundColor: selectedColor, width: '140px', height: '50px', display: 'flex', justifyContent: 'center', alignItems: 'center' }
    }, 
      vero('p', { style: { backgroundColor: 'white', padding: '4px', color: selectedColor, fontWeight: 'bold', borderRadius: '4px' } }, `Color: ${selectedColor}`)
    ),
    vero('h4', null, 'Pick a color:'),
    vero('div', { style: { display: 'flex', gap: '10px' } },
      ...colors.map(color =>
        vero('button', {
          key: color,
          className: 'color-box',
          style: { backgroundColor: color, width: '100px', height: '50px', outline: 'none', border: 'none' },
          onClick: () => {
            console.log(`Color ${color} clicked`);
            setSelectedColor(color)
          }
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
