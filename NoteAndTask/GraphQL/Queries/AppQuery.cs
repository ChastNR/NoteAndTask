using System.Linq;
using GraphQL.Types;
using NoteAndTask.GraphQL.Types;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IRepository repository, string authenticatedUserId)
        {
            Field<ListGraphType<TaskListType>>(
                "lists",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the list" },
                    new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the list" },
                    new QueryArgument<StringGraphType> { Name = "userId", Description = "Lists with userId" },
                    new QueryArgument<DateTimeGraphType> { Name = "creationDate", Description = "Date of creation" }),
                resolve: context =>
                {
                    if (string.IsNullOrWhiteSpace(authenticatedUserId))
                    {
                        var id = context.GetArgument<string>("id");
                        if (id != null)
                        {
                            return repository.Get<TaskList>(l => l.Id == id);
                        }

                        var name = context.GetArgument<string>("name");
                        if (name != null)
                        {
                            return repository.Get<TaskList>(l => l.Name == name);
                        }

                        var userId = context.GetArgument<string>("userId");
                        if (userId != null)
                        {
                            return repository.Get<TaskList>(l => l.UserId == userId);
                        }

                        var creationDate = context.GetArgument<string>("creationDate");
                        if (creationDate != null)
                        {
                            return repository.Get<TaskList>(l => l.CreationDate.ToString() == creationDate);
                        }

                        return repository.GetAll<TaskList>(null, "Tasks");
                    }
                    else
                    {
                        var id = context.GetArgument<string>("id");
                        if (id != null)
                        {
                            return repository.Get<TaskList>(l => l.Id == id && l.UserId == authenticatedUserId);
                        }

                        var name = context.GetArgument<string>("name");
                        if (name != null)
                        {
                            return repository.Get<TaskList>(l => l.Name == name && l.UserId == authenticatedUserId);
                        }

                        var userId = context.GetArgument<string>("userId");
                        if (userId != null)
                        {
                            return repository.Get<TaskList>(l => l.UserId == userId && l.UserId == authenticatedUserId);
                        }

                        var creationDate = context.GetArgument<string>("creationDate");
                        if (creationDate != null)
                        {
                            return repository.Get<TaskList>(l => l.CreationDate.ToString() == creationDate && l.UserId == authenticatedUserId);
                        }

                        return repository.GetAll<TaskList>(null, "Tasks").Where(l => l.UserId == authenticatedUserId);
                    }
                });

            Field<ListGraphType<TaskEntityType>>(
                    "tasks",
                    arguments: new QueryArguments(
                        new QueryArgument<IdGraphType> { Name = "id", Description = "The id of the task" },
                        new QueryArgument<StringGraphType> { Name = "name", Description = "The name of the task" }),
                    resolve: context =>
                    {
                        if (string.IsNullOrWhiteSpace(authenticatedUserId))
                        {
                            var id = context.GetArgument<string>("id");

                            if (id != null)
                            {
                                return repository.Get<TaskEntity>(t => t.Id == id);
                            }

                            var name = context.GetArgument<string>("name");

                            if (name != null)
                            {
                                return repository.Get<TaskEntity>(t => t.Name == name);
                            }

                            return repository.GetAll<TaskEntity>();
                        }
                        else
                        {
                            var id = context.GetArgument<string>("id");

                            if (id != null)
                            {
                                return repository.Get<TaskEntity>(t => t.Id == id && t.UserId == authenticatedUserId);
                            }

                            var name = context.GetArgument<string>("name");

                            if (name != null)
                            {
                                return repository.Get<TaskEntity>(t => t.Name == name && t.UserId == authenticatedUserId);
                            }

                            return repository.GetAll<TaskEntity>().Where(t => t.UserId == authenticatedUserId);
                        }
                    });

            Field<ListGraphType<UserType>>(
                "users",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" },
                    new QueryArgument<StringGraphType> { Name = "name" },
                    new QueryArgument<StringGraphType> { Name = "phoneNumber" },
                    new QueryArgument<StringGraphType> { Name = "email" },
                    new QueryArgument<StringGraphType> { Name = "password" },
                    new QueryArgument<StringGraphType> { Name = "creationDate" },
                    new QueryArgument<BooleanGraphType> { Name = "confirmed" }
                    ),
                resolve: context =>
                {
                    if (!string.IsNullOrWhiteSpace(authenticatedUserId))
                        return repository.GetAll<User>(null, "Tasks").Where(u => u.Id == authenticatedUserId);
                    var id = context.GetArgument<string>("id");
                    if (id != null)
                    {
                        return repository.Get<User>(l => l.Id == id);
                    }

                    var name = context.GetArgument<string>("name");
                    if (name != null)
                    {
                        return repository.Get<User>(l => l.Name == name);
                    }

                    var phoneNumber = context.GetArgument<string>("phoneNumber");
                    if (phoneNumber != null)
                    {
                        return repository.Get<User>(l => l.PhoneNumber == phoneNumber);
                    }

                    var creationDate = context.GetArgument<string>("creationDate");
                    if (creationDate != null)
                    {
                        return repository.Get<TaskList>(l => l.CreationDate.ToString() == creationDate);
                    }

                    var password = context.GetArgument<string>("password");
                    if (password != null)
                    {
                        return repository.Get<User>(l => l.PasswordHash == password);
                    }

                    var email = context.GetArgument<string>("email");
                    if (email != null)
                    {
                        return repository.Get<User>(l => l.Email == email);
                    }

                    var confirmed = context.GetArgument<bool>("confirmed");
                    if (confirmed)
                    {
                        return repository.Get<User>(l => l.Confirmed == confirmed);
                    }

                    return repository.GetAll<User>(null, "Tasks");
                });
        }
    }
}