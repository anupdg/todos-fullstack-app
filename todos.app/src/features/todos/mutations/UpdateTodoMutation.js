import { commitMutation } from 'react-relay';
import RelayEnvironment from '../../../utils/RelayEnvironment';
import UpdateTodoMutation from '../../../__generated__/operationsUpdateTodoMutation.graphql';

export function updateTodo(id, input, onCompleted, onError) {
  return commitMutation(RelayEnvironment, {
    mutation: UpdateTodoMutation,
    variables: { id, input },
    onCompleted,
    onError,
  });
}
