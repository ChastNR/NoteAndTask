using GraphQL.Types;
using ProjectModels;

namespace NoteAndTask.GraphQL.Types
{
    public class TaskListType : ObjectGraphType<TaskList>
    {
        public TaskListType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("List Id");
            Field(x => x.Name);
            Field(x => x.UserId);
            Field(x => x.CreationDate);
            Field(x => x.Tasks, type: typeof(ListGraphType<TaskEntityType>)).Description("Tasks");
        }
    }
}