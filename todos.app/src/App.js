import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { TodosList, CreateTodoModal } from './features/todos';

function App() {
  const [refreshKey, setRefreshKey] = React.useState(0);
  const handleTodoCreated = () => setRefreshKey(k => k + 1);
  return (
    <div className="container py-5">
      <h1 className="mb-4">Todo List</h1>
      <CreateTodoModal onTodoCreated={handleTodoCreated} />
      <TodosList refreshKey={refreshKey} />
    </div>
  );
}

export default App;
