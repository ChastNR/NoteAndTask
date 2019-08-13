using System.Threading.Tasks;
using GraphQL.Types;
using NoteAndTask.GraphQL.Types;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.GraphQL.Queries
{
    public class TaskListQuery : ObjectGraphType
    {
        public TaskListQuery(IRepository repo)
        {
            var repository = repo;

            Field<TaskListType>(
                "List",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id", Description = "List Id"}),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");

                    return repository.GetFirst<TaskList>(l => l.Id == id, null, "tasks");

                });

            Field<ListGraphType<TaskEntityType>>(
                "Tasks",
                resolve: context => repository.GetAll<TaskList>(null, "tasks"));
        }
    }
}