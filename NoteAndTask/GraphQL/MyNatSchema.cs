using System;
using GraphQL;
using GraphQL.Types;

namespace NoteAndTask.GraphQL
{
    public class NatSchema : Schema
	{
        public NatSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<NatQuery>();
        }
    }
}
