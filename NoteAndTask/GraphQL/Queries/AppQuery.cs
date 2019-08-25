using GraphQL.Types;
using NoteAndTask.GraphQL.Types;
using EFRepository.Interface;
using ProjectModels;

namespace NoteAndTask.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IRepository repository)
        {
            Field<ListGraphType<TaskListType>>(
                "lists",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the list" },
                    new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the list" },
                    new QueryArgument<IntGraphType> { Name = "userId", Description = "Lists with userId" }
                    //new QueryArgument<StringGraphType> { Name = "creationDate", Description = "Date of creation" }
                    ),
                resolve: context =>
                {
                        var id = context.GetArgument<int>("id");
                        if (id > 0)
                        {
                            return repository.Get<TaskList>(l => l.Id == id);
                        }

                        var name = context.GetArgument<string>("name");
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            return repository.Get<TaskList>(l => l.Name == name);
                        }

                        var userId = context.GetArgument<int>("userId");
                        if (userId > 0)
                        {
                            return repository.Get<TaskList>(l => l.UserId == userId);
                        }

//                        var creationDate = context.GetArgument<DateTime>("creationDate");
//                        if (creationDate != null)
//                        {
//                            return repository.Get<TaskList>(l => l.CreationDate == creationDate);
//                        }

                        return repository.GetAll<TaskList>(null, "Tasks");
                    
                    
                });

            Field<ListGraphType<TaskEntityType>>(
                    "tasks",
                    arguments: new QueryArguments(
                        new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the task" },
                        new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the task" }),
                    resolve: context =>
                    {
                            var id = context.GetArgument<int>("id");
                            if (id > 0)
                            {
                                return repository.Get<TaskEntity>(t => t.Id == id);
                            }

                            var name = context.GetArgument<string>("name");
                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                return repository.Get<TaskEntity>(t => t.Name == name);
                            }

                            return repository.GetAll<TaskEntity>();
                        
                    });
        }
    }
}


#region Backup

//using System.Linq;
//using EFRepository.Interface;
//using EFRepository.Models;
//using GraphQL.Types;
//using NoteAndTask.GraphQL.Types;
//
//namespace NoteAndTask.GraphQL.Queries
//{
//    public class AppQuery : ObjectGraphType
//    {
//        public AppQuery(IRepository repository , int authenticatedUserId)
//        {
//            Field<ListGraphType<TaskListType>>(
//                "lists",
//                arguments: new QueryArguments(
//                    new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the list" },
//                    new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the list" },
//                    new QueryArgument<IntGraphType> { Name = "userId", Description = "Lists with userId" },
//                    new QueryArgument<DateTimeGraphType> { Name = "creationDate", Description = "Date of creation" }),
//                resolve: context =>
//                {
//                        var id = context.GetArgument<int>("id");
//                        if (id != null)
//                        {
//                            return repository.Get<TaskList>(l => l.Id == id && l.UserId == authenticatedUserId);
//                        }
//
//                        var name = context.GetArgument<string>("name");
//                        if (name != null)
//                        {
//                            return repository.Get<TaskList>(l => l.Name == name && l.UserId == authenticatedUserId);
//                        }
//
//                        var creationDate = context.GetArgument<string>("creationDate");
//                        if (creationDate != null)
//                        {
//                            return repository.Get<TaskList>(l => l.CreationDate.ToString() == creationDate && l.UserId == authenticatedUserId);
//                        }
//
//                        return repository.GetAll<TaskList>(null, "Tasks").Where(l => l.UserId == authenticatedUserId);
//                    
//                });
//
//            Field<ListGraphType<TaskEntityType>>(
//                    "tasks",
//                    arguments: new QueryArguments(
//                        new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the task" },
//                        new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the task" }),
//                    resolve: context =>
//                    {
//                            var id = context.GetArgument<int>("id");
//
//                            if (id != null)
//                            {
//                                return repository.Get<TaskEntity>(t => t.Id == id && t.UserId == authenticatedUserId);
//                            }
//
//                            var name = context.GetArgument<string>("name");
//
//                            if (name != null)
//                            {
//                                return repository.Get<TaskEntity>(t => t.Name == name && t.UserId == authenticatedUserId);
//                            }
//
//                            return repository.GetAll<TaskEntity>().Where(t => t.UserId == authenticatedUserId);
//                        
//                    });
//        }
//    }

//}
#endregion
