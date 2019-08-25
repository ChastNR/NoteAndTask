using System;
using System.Threading.Tasks;
using EFRepository.Interface;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteAndTask.GraphQL.Queries;

namespace NoteAndTask.Controllers
{
    [Route("api/[Controller]")]
    //[Authorize]
    [ApiController]
    public class DataController : Controller
    {
        private readonly IRepository _repository;

            public DataController(IRepository repository) => _repository = repository;

            public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
            {
                var inputs = query.Variables.ToInputs();

                var schema = new Schema
                {
                    Query = new AppQuery(_repository)
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