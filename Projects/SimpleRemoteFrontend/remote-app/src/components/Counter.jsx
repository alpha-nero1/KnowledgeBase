import React, { useState } from 'react'

const Counter = () => {
  const [count, setCount] = useState(0)

  return (
    <div style={{ 
      border: '2px solid #2196F3', 
      padding: '15px', 
      borderRadius: '8px',
      margin: '10px 0',
      textAlign: 'center'
    }}>
      <h3>Remote Counter Component</h3>
      <p>Count: {count}</p>
      <button 
        onClick={() => setCount(count + 1)}
        style={{
          backgroundColor: '#2196F3',
          color: 'white',
          padding: '8px 16px',
          border: 'none',
          borderRadius: '4px',
          cursor: 'pointer',
          margin: '0 5px'
        }}
      >
        +
      </button>
      <button 
        onClick={() => setCount(count - 1)}
        style={{
          backgroundColor: '#f44336',
          color: 'white',
          padding: '8px 16px',
          border: 'none',
          borderRadius: '4px',
          cursor: 'pointer',
          margin: '0 5px'
        }}
      >
        -
      </button>
    </div>
  )
}

export default Counter
