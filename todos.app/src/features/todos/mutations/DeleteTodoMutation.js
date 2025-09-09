import { commitMutation } from 'react-relay';
import RelayEnvironment from '../../../utils/RelayEnvironment';
import DeleteTodoMutation from '../../../__generated__/operationsDeleteTodoMutation.graphql';

export function deleteTodo(id, onCompleted, onError) {
  return commitMutation(RelayEnvironment, {
    mutation: DeleteTodoMutation,
    variables: { id },
    onCompleted,
    onError,
  });
}
