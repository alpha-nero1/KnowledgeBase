import React, { Suspense } from 'react'

// Import remote components from the remote app
const RemoteButton = React.lazy(() => import('remoteApp/Button'))
const RemoteCounter = React.lazy(() => import('remoteApp/Counter'))

function App() {
  return (
    <div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto' }}>
      <h1>Host Application</h1>
      <p>This is the main host application that consumes microfrontends.</p>
      
      <div style={{ 
        border: '2px solid #9C27B0', 
        padding: '20px', 
        borderRadius: '8px',
        marginBottom: '20px',
        backgroundColor: '#f8f9fa'
      }}>
        <h2>Local Host Content</h2>
        <p>This content is rendered by the host application itself.</p>
        <button style={{
          backgroundColor: '#9C27B0',
          color: 'white',
          padding: '10px 20px',
          border: 'none',
          borderRadius: '4px',
          cursor: 'pointer'
        }}>
          Host Button
        </button>
      </div>

      <div style={{ 
        border: '2px solid #FF9800', 
        padding: '20px', 
        borderRadius: '8px',
        backgroundColor: '#fff3e0'
      }}>
        <h2>Remote Microfrontend Components</h2>
        <p>The components below are loaded from the remote microfrontend:</p>
        
        <Suspense fallback={<div>Loading Remote Button...</div>}>
          <RemoteButton />
        </Suspense>
        
        <Suspense fallback={<div>Loading Remote Counter...</div>}>
          <RemoteCounter />
        </Suspense>
      </div>
    </div>
  )
}

export default App
