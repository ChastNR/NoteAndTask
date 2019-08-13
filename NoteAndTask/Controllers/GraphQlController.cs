using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using NoteAndTask.GraphQL.Queries;
using Repository.Interface;

namespace NoteAndTask.Controllers
{
    [Route("graphql")]
    [ApiController]
    public class GraphQlController : Controller
    {
        private readonly IRepository _repository;

        public GraphQlController(IRepository repository) => _repository = repository;

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            var schema = new Schema
            {
                Query = new TaskListQuery(_repository)
            };

            var result = await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            });
            
            if(result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}