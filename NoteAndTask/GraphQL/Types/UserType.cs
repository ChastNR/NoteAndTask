using GraphQL.Types;
using ProjectModels;

namespace NoteAndTask.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("User Id");
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
            Field(x => x.Tasks, type: typeof(ListGraphType<TaskEntityType>)).Description("Tasks");
        }
    }
}