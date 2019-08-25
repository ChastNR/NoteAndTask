using GraphQL;
using GraphQL.Types;
using NoteAndTask.GraphQL.Queries;

namespace NoteAndTask.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<AppQuery>();
        }
    }
}
