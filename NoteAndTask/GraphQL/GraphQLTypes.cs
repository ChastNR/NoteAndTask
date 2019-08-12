using System;
using GraphQL.Types;
using Repository.Models;

namespace NoteAndTask.GraphQL
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.PhoneNumber);
            Field(x => x.Email);
            Field(x => x.PasswordHash);
            Field(x => x.CreationDate);
            Field(x => x.Confirmed);
            Field(x => x.EmailNotifications);
            Field(x => x.SmsNotifications);
            Field(x => x.Token);
            Field(x => x.UserLogoPath);
            Field<TaskEntityType>(nameof(User.Tasks));
        }
    }

    public class TaskListType : ObjectGraphType<TaskList>
    {
        public TaskListType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.CreationDate);
            Field(x => x.UserId);
            Field<UserType>(nameof(TaskList.User));
            Field<TaskEntityType>(nameof(TaskList.Tasks));
        }
    }

    public class TaskEntityType : ObjectGraphType<TaskEntity>
    {
        public TaskEntityType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.CreationDate);
            Field(x => x.ExpiresOn);
            Field(x => x.IsDone);
            Field(x => x.TaskListId);
            Field(x => x.UserId);
            Field<TaskListType>(nameof(TaskEntity.TaskList));
            Field<UserType>(nameof(TaskEntity.User));
        }
    }
}
