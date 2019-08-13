using GraphQL.Types;
using Repository.Models;

namespace NoteAndTask.GraphQL.Types
{
    public class TaskListType : ObjectGraphType<TaskList>
    {
        public TaskListType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("List Id");
            Field(x => x.Name);
            Field(x => x.CreationDate);
            //Field(x => x.UserId);
            Field(x => x.Tasks, type: typeof(ListGraphType<TaskEntityType>)).Description("Tasks");
            // Field<UserType>(nameof(TaskList.User));
            // Field<TaskEntityType>(nameof(TaskList.Tasks));
        }
    }
}