using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Application.Commands;
using StargateAPI.Application.Queries;
using System.Net;

namespace StargateAPI.Api.Controllers
{


[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IMediator _mediator;
    public PersonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllPeople()
    {
        try
        {
            var result = await _mediator.Send(new GetPeople()
            {

            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetPersonByName([FromRoute] string name)
    {
        try
        {
            var result = await _mediator.Send(new GetPersonByName()
            {
                Name = name
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePerson([FromBody] string name)
    {
        try
        {
            var result = await _mediator.Send(new CreatePerson()
            {
                Name = name
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }
}
}