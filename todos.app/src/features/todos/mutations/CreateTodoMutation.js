import { commitMutation } from 'react-relay';
import RelayEnvironment from '../../../utils/RelayEnvironment';
import CreateTodoMutation from '../../../__generated__/operationsCreateTodoMutation.graphql';

export function createTodo(input, onCompleted, onError) {
  return commitMutation(RelayEnvironment, {
    mutation: CreateTodoMutation,
    variables: { input },
    onCompleted,
    onError,
  });
}
