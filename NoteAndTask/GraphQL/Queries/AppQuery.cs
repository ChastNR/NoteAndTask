//using System;
//using System.Linq;
//using GraphQL.Types;
//using NoteAndTask.GraphQL.Types;
//using Repository.Interface;
//using Repository.Models;
//
//namespace NoteAndTask.GraphQL.Queries
//{
//    public class AppQuery : ObjectGraphType
//    {
//        public AppQuery(IListRepository listRepository,ITaskRepository taskRepository , int authenticatedUserId)
//        {
//            Field<ListGraphType<TaskListType>>(
//                "lists",
//                arguments: new QueryArguments(
//                    new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the list" },
//                    new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the list" },
//                    new QueryArgument<GuidGraphType> { Name = "userId", Description = "Lists with userId" },
//                    new QueryArgument<DateTimeGraphType> { Name = "creationDate", Description = "Date of creation" }),
//                resolve: context =>
//                {
//                        var id = context.GetArgument<int>("id");
//                        if (id != null)
//                        {
//                            return repository.Get<TaskList>(l => l.Id == id && l.UserId == authenticatedUserId);
//                            return listRepository.Get(authenticatedUserId)
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