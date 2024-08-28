using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Queries;
using StargateAPI.Api.Models;
using System.Net;

namespace StargateAPI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AstronautDutyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AstronautDutyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetAstronautDutiesByName(string name)
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
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDutyRequest request)
        {
            //add error handling
                var result = await _mediator.Send(request);
                return Ok(result);          
        }
    }
}