import { h, Fragment, useState, createApp } from './vero/index.js';

// Counter Component with JSX syntax
function Counter() {
  const [count, setCount] = useState(0);

  return (
    <div>
      <h3>Count: {count}</h3>
      <button 
        className="counter-btn"
        onClick={() => setCount(count + 1)}
      >
        +
      </button>
      <button 
        className="counter-btn"
        onClick={() => setCount(count - 1)}
      >
        -
      </button>
      <button 
        className="counter-btn"
        onClick={() => setCount(0)}
      >
        Reset
      </button>
      <p>Click the buttons to update the state!</p>
    </div>
  );
}

// Todo App Component with JSX
function TodoApp() {
  const [todos, setTodos] = useState([
    { id: 1, text: 'Learn Virtual DOM', completed: false },
    { id: 2, text: 'Build simple framework', completed: true }
  ]);
  const [newTodo, setNewTodo] = useState('');

  const addTodo = () => {
    if (newTodo.trim()) {
      setTodos([...todos, {
        id: Date.now(),
        text: newTodo.trim(),
        completed: false
      }]);
      setNewTodo('');
    }
  };

  const toggleTodo = (id) => {
    setTodos(todos.map(todo =>
      todo.id === id ? { ...todo, completed: !todo.completed } : todo
    ));
  };

  const removeTodo = (id) => {
    setTodos(todos.filter(todo => todo.id !== id));
  };

  return (
    <div>
      <input
        className="input-field"
        type="text"
        placeholder="Add a new todo..."
        value={newTodo}
        onInput={(e) => setNewTodo(e.target.value)}
        onKeyPress={(e) => {
          if (e.key === 'Enter') addTodo();
        }}
      />
      <button className="counter-btn" onClick={addTodo}>
        Add Todo
      </button>
      
      <div>
        {todos.map(todo => (
          <div
            key={todo.id}
            className={`todo-item ${todo.completed ? 'completed' : ''}`}
          >
            <input
              type="checkbox"
              checked={todo.completed}
              onChange={() => toggleTodo(todo.id)}
            />
            <span>{todo.text}</span>
            <button
              onClick={() => removeTodo(todo.id)}
              style={{
                marginLeft: 'auto',
                background: '#dc3545',
                color: 'white',
                border: 'none',
                padding: '4px 8px',
                borderRadius: '3px',
                cursor: 'pointer'
              }}
            >
              Remove
            </button>
          </div>
        ))}
      </div>
      
      <p>Total: {todos.length}, Completed: {todos.filter(t => t.completed).length}</p>
    </div>
  );
}

// Simple Timer Component
function Timer() {
  const [seconds, setSeconds] = useState(0);
  const [isRunning, setIsRunning] = useState(false);

  // Simple useEffect simulation
  const [intervalId, setIntervalId] = useState(null);

  const startTimer = () => {
    if (!isRunning) {
      const id = setInterval(() => {
        setSeconds(prev => prev + 1);
      }, 1000);
      setIntervalId(id);
      setIsRunning(true);
    }
  };

  const stopTimer = () => {
    if (intervalId) {
      clearInterval(intervalId);
      setIntervalId(null);
      setIsRunning(false);
    }
  };

  const resetTimer = () => {
    stopTimer();
    setSeconds(0);
  };

  return (
    <div>
      <h3>Timer: {Math.floor(seconds / 60)}:{(seconds % 60).toString().padStart(2, '0')}</h3>
      <button className="counter-btn" onClick={startTimer} disabled={isRunning}>
        Start
      </button>
      <button className="counter-btn" onClick={stopTimer} disabled={!isRunning}>
        Stop
      </button>
      <button className="counter-btn" onClick={resetTimer}>
        Reset
      </button>
      <p>Status: {isRunning ? 'Running' : 'Stopped'}</p>
    </div>
  );
}

// Mount all apps
document.addEventListener('DOMContentLoaded', () => {
  // Counter app
  const counterApp = createApp(Counter, document.getElementById('counter-app'));
  counterApp.mount();

  // Todo app
  const todoApp = createApp(TodoApp, document.getElementById('todo-app'));
  todoApp.mount();

  // Timer app (if element exists)
  const timerElement = document.getElementById('timer-app');
  if (timerElement) {
    const timerApp = createApp(Timer, timerElement);
    timerApp.mount();
  }
});
