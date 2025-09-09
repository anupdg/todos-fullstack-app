// This file forces Relay to recognize the GraphQL operations
import { graphql } from 'react-relay';

// Force relay to compile these operations
export const TodosListQuery = graphql`
  query operationsTodosListQuery {
    todos {
      id
      title
      description
      isCompleted
      status
    }
  }
`;

export const CreateTodoMutation = graphql`
  mutation operationsCreateTodoMutation($input: CreateTodoInput!) {
    createTodo(input: $input) {
      id
      title
      description
      isCompleted
      status
    }
  }
`;

export const UpdateTodoMutation = graphql`
  mutation operationsUpdateTodoMutation($id: UUID!, $input: UpdateTodoInput!) {
    updateTodo(id: $id, input: $input) {
      id
      title
      description
      isCompleted
      status
    }
  }
`;

export const DeleteTodoMutation = graphql`
  mutation operationsDeleteTodoMutation($id: UUID!) {
    deleteTodo(id: $id)
  }
`;
