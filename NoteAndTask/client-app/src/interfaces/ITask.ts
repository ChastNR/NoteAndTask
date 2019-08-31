export interface ITask {
  id: number;
  name: string;
  description: string;
  isDone: boolean;
  expiresOn: Date;
  taskListId?: number;
}
