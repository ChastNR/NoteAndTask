using GraphQL.Types;
using Repository.Models;

namespace NoteAndTask.GraphQL.Types
{
    public class TaskEntityType : ObjectGraphType<TaskEntity>
    {
        public TaskEntityType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Task Id");
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.IsDone);
            Field(x => x.CreationDate);
            Field(x => x.ExpiresOn);
            Field(x => x.TaskListId);
            Field(x => x.UserId);

        }
    }
}