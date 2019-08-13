using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using GraphQL.Types;
using Repository.Interface;
using Repository.Models;

namespace NoteAndTask.GraphQL
{
    public class NatQuery : ObjectGraphType
    {
        //private readonly IRepository _repository;

        public NatQuery(IRepository repo)
        {
            var repository = repo;

            Field<ListGraphType<TaskListType>>("lists",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = "id"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "name"
                    },
                    new QueryArgument<StringGraphType>
                    {
                        Name = "userId"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name = "creationDate"
                    }
                }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    var isUserAuthenticated = ((ClaimsIdentity)user.Identity).IsAuthenticated;

                    var query = repository.GetAll<TaskList>();

                    var listId = context.GetArgument<string>("id");

                    if (string.IsNullOrEmpty(listId))
                    {
                        return repository.GetAll<TaskList>().Where(l => l.Id == listId);
                    }

                    var name = context.GetArgument<string>("name");

                    if (string.IsNullOrEmpty(name))
                    {
                        return repository.GetAll<TaskList>().Where(l => l.Name == name);
                    }

                    var userId = context.GetArgument<string>("userId");

                    if (string.IsNullOrEmpty(userId))
                    {
                        return repository.GetAll<TaskList>().Where(l => l.UserId == userId);
                    }

                    var creationDate = context.GetArgument<DateTime?>("creationDate");

                    if (creationDate.HasValue)
                    {
                        return repository.GetAll<TaskList>().Where(l => l.CreationDate == creationDate);
                    }

                    return query.ToList();
                });
        }
    }
}
