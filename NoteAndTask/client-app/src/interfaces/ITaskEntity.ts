export interface ITaskEntity {
  Id: number;
  Name: string;
  Description: string;
  IsDone: boolean;
  ExpiresOn: Date;
  CreationDate: Date;
  TaskListId: number;
  UserId: number;
}
