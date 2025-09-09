import React, { useState, useEffect } from 'react';
import { updateTodo } from '../mutations/UpdateTodoMutation';

export default function UpdateTodoModal({ show, onClose, todo, onTodoUpdated }) {
  const [form, setForm] = useState({
    title: '',
    description: '',
    status: 'Pending',
    isCompleted: false,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    if (todo) {
      setForm({
        title: todo.title,
        description: todo.description,
        status: todo.status,
        isCompleted: todo.isCompleted,
      });
    }
  }, [todo]);

  const handleChange = e => {
    const { name, value, type, checked } = e.target;
    setForm(f => ({
      ...f,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSubmit = e => {
    e.preventDefault();
    setLoading(true);
    setError(null);
    updateTodo(
      todo.id,
      form,
      () => {
        setLoading(false);
        onClose();
        if (onTodoUpdated) onTodoUpdated();
      },
      err => {
        setLoading(false);
        setError('Failed to update todo');
      }
    );
  };

  if (!show) return null;

  return (
    <div className="modal show d-block" tabIndex="-1" role="dialog">
      <div className="modal-dialog" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Update Todo</h5>
            <button type="button" className="btn-close" onClick={onClose} aria-label="Close"></button>
          </div>
          <form onSubmit={handleSubmit}>
            <div className="modal-body">
              {error && <div className="alert alert-danger">{error}</div>}
              <div className="mb-3">
                <label className="form-label">Title</label>
                <input 
                  type="text" 
                  className="form-control" 
                  name="title" 
                  value={form.title} 
                  onChange={handleChange} 
                  required 
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Description</label>
                <textarea 
                  className="form-control" 
                  name="description" 
                  value={form.description} 
                  onChange={handleChange} 
                  required 
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Status</label>
                <select 
                  className="form-select" 
                  name="status" 
                  value={form.status} 
                  onChange={handleChange}
                >
                  <option value="Pending">Pending</option>
                  <option value="Completed">Completed</option>
                </select>
              </div>
              <div className="form-check mb-3">
                <input 
                  className="form-check-input" 
                  type="checkbox" 
                  name="isCompleted" 
                  checked={form.isCompleted} 
                  onChange={handleChange} 
                  id="updateIsCompletedCheck" 
                />
                <label className="form-check-label" htmlFor="updateIsCompletedCheck">
                  Is Completed
                </label>
              </div>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-secondary" onClick={onClose}>
                Cancel
              </button>
              <button type="submit" className="btn btn-primary" disabled={loading}>
                {loading ? 'Updating...' : 'Update'}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
