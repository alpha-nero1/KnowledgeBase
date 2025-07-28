import React from 'react'
import Button from './components/Button'
import Counter from './components/Counter'

function App() {
  return (
    <div style={{ padding: '20px' }}>
      <h1>Remote App</h1>
      <p>This is the remote microfrontend application.</p>
      <div style={{ marginBottom: '20px' }}>
        <h3>Exposed Components:</h3>
        <Button />
        <Counter />
      </div>
    </div>
  )
}

export default App
