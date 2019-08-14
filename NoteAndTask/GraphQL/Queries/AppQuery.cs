using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using NoteAndTask.GraphQL.Types;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IRepository repository)
        {
            Field<ListGraphType<TaskListType>>(
                "lists",
                resolve: context => repository.GetAll<TaskList>(null, "Tasks"));

            Field<TaskEntityType>(
                "task",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id", Description = "The ID of the task."},
                    new QueryArgument<StringGraphType> {Name = "name", Description = "The name of task"}),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    
                    if (id != null)
                    {
                        return repository.GetById<TaskEntity>(id);
                    }
                    
                    var name = context.GetArgument<string>("name");

                    if (name != null)
                    {
                        return repository.GetFirst<TaskEntity>(t => t.Name == name);
                    }

                    return false;
                });
        }
    }
}