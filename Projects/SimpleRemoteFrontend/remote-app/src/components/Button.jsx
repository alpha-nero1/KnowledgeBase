import React from 'react'

const Button = () => {
  const handleClick = () => {
    alert('Button clicked from Remote App!')
  }

  return (
    <button 
      onClick={handleClick}
      style={{
        backgroundColor: 'blue',
        color: 'white',
        padding: '10px 20px',
        border: 'none',
        borderRadius: '4px',
        cursor: 'pointer',
        margin: '5px'
      }}
    >
      Remote Button
    </button>
  )
}

export default Button
