import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { useHubConnection } from './lib/useHubConnection';
import { Api } from './lib/api';

function App() {
  const [from, setFrom] = useState("");
  const [to, setTo] = useState("leo");
  const [message, setMessage] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [usr, setUsr] = useState("");
  const [pass, setPass] = useState("");

  const hub = useHubConnection(usr, isLoggedIn);

  const messageOnSend = () => {
    hub.sendMessage(to, message);
  }

  const loginOnClick = () => {
    Api().login(usr, pass)
    .then(res => {
        localStorage.setItem('access_token', res.token);
        setIsLoggedIn(true);
    })
    .catch(() => {
        setIsLoggedIn(false);
    })
  }

  const connDesc = () => {
    if (hub.isLoading) return 'Loading...';
    if (hub.connectionSevered) return 'Severed X';
    if (!isLoggedIn) return 'Must log in';
    return 'Live';
  }

  return (
    <>
      <div className="card">
        <h1>Chat</h1>
        <div className='row'>
            <div>Connection: {connDesc()}</div>
            {
                hub.connectionSevered && !hub.isLoading ?
                <button onClick={hub.connect}>Reconnect</button>
                : null
            }
        </div>
        <div>
            {
                isLoggedIn ? <div>Hello: {from}</div> :
                <div>
                    <h2>Login</h2>
                    <input placeholder='Username' value={usr} onChange={e => setUsr(e.target.value)}></input>
                    <input type='password' placeholder='Password' value={pass} onChange={e => setPass(e.target.value)}></input>
                    <button onClick={loginOnClick}>Login</button>
                </div>
            }
        </div>
        <div className='row'>
            <p>To: </p>
            <input placeholder='To' value={to} onChange={e => setTo(e.target.value)}></input>
        </div>
        <div className='row'>
            <input placeholder='Send message' value={message} onChange={e => setMessage(e.target.value)}></input>
            <button onClick={messageOnSend}>Send</button>
        </div>
        <div className='col'>
            <h2>Messages</h2>
            <div>
                {hub.messages.map(msg => 
                    <p key={`${msg.user} - ${msg.message}`}>
                        {msg.user} - {msg.message}
                    </p>
                )}
            </div>
        </div>
      </div>
    </>
  )
}

export default App
