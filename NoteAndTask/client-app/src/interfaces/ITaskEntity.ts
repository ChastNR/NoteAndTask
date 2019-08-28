export interface ITaskEntity {
  id: number;
  name: string;
  description: string;
  isDone: boolean;
  expiresOn: Date;
  creationDate: Date;
  taskListId?: number;
  userId: number;
}
