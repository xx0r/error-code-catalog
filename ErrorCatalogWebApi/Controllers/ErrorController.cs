using System.ComponentModel;
using ErrorCatalogWebApi.Domain.Errors;
using ErrorCatalogWebApi.SeedWork;
using Microsoft.AspNetCore.Mvc;
namespace ErrorCatalogWebApi.Controllers;

[ApiController, Route("error")]
public class ErrorController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        ExecuteCommandHandler();

        return Ok();

        // This should be a command handler
        void ExecuteCommandHandler()
        {
            throw new DomainException<AccountError>(e => e.AccountAlreadyExists);
        }
    }

    [HttpGet("group/{groupName}")]
    public IActionResult GetGroup([FromRoute, DefaultValue("SomeTestGroupName")] string groupName)
    {
        ExecuteCommandHandler();

        return Ok();

        // This should be a command handler
        void ExecuteCommandHandler()
        {
            throw new DomainException<GroupError>(e => e.GroupNotFound, groupName);
        }
    }
}
