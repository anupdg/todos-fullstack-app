import React, { useState } from 'react';
import { useLazyLoadQuery } from 'react-relay';
import { deleteTodo } from '../mutations/DeleteTodoMutation';
import ConfirmDeleteModal from './ConfirmDeleteModal';
import UpdateTodoModal from './UpdateTodoModal';
import TodosListQuery from '../../../__generated__/operationsTodosListQuery.graphql';

export default function TodosList({ refreshKey }) {
  const [deletingId, setDeletingId] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [todoToDelete, setTodoToDelete] = useState(null);
  const [showUpdateModal, setShowUpdateModal] = useState(false);
  const [todoToUpdate, setTodoToUpdate] = useState(null);
  const data = useLazyLoadQuery(
    TodosListQuery,
    {},
    { fetchPolicy: "network-only", fetchKey: refreshKey }
  );

  const handleDeleteClick = (todo) => {
    setTodoToDelete(todo);
    setShowDeleteModal(true);
  };

  const handleEditClick = (todo) => {
    setTodoToUpdate(todo);
    setShowUpdateModal(true);
  };

  const handleCloseUpdateModal = () => {
    setShowUpdateModal(false);
    setTodoToUpdate(null);
  };

  const handleTodoUpdated = () => {
    // Trigger a refetch by reloading the page (temporary solution)
    window.location.reload();
  };

  const handleConfirmDelete = () => {
    if (!todoToDelete) return;
    
    setDeletingId(todoToDelete.id);
    setShowDeleteModal(false);
    deleteTodo(
      todoToDelete.id,
      () => {
        setDeletingId(null);
        setTodoToDelete(null);
        // Trigger a refetch by updating the parent's refresh key
        window.location.reload(); // Temporary solution for demo
      },
      (error) => {
        setDeletingId(null);
        setTodoToDelete(null);
        alert('Failed to delete todo');
        console.error(error);
      }
    );
  };

  const handleCancelDelete = () => {
    setShowDeleteModal(false);
    setTodoToDelete(null);
  };

  if (!data.todos || data.todos.length === 0) {
    return <div className="alert alert-info">No todos found.</div>;
  }

  return (
    <>
      <div className="row">
        {data.todos.map(todo => (
          <div className="col-md-6 col-lg-4 mb-4" key={todo.id}>
            <div className="card h-100">
              <div className="card-body">
                <h5 className="card-title">{todo.title}</h5>
                <p className="card-text">{todo.description}</p>
                <span className={`badge ${todo.isCompleted ? 'bg-success' : 'bg-warning text-dark'}`}>{todo.isCompleted ? 'Completed' : 'Pending'}</span>
                <span className="ms-2 badge bg-secondary">{todo.status}</span>
              </div>
              <div className="card-footer">
                <button 
                  className="btn btn-primary btn-sm me-2"
                  onClick={() => handleEditClick(todo)}
                >
                  Edit
                </button>
                <button 
                  className="btn btn-danger btn-sm"
                  onClick={() => handleDeleteClick(todo)}
                  disabled={deletingId === todo.id}
                >
                  {deletingId === todo.id ? 'Deleting...' : 'Delete'}
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
      
      <ConfirmDeleteModal
        show={showDeleteModal}
        onConfirm={handleConfirmDelete}
        onCancel={handleCancelDelete}
        todoTitle={todoToDelete?.title || ''}
      />
      
      <UpdateTodoModal
        show={showUpdateModal}
        onClose={handleCloseUpdateModal}
        todo={todoToUpdate}
        onTodoUpdated={handleTodoUpdated}
      />
    </>
  );
}
