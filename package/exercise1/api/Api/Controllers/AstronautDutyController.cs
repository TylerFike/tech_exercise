using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using StargateAPI.Api.Models;
using StargateAPI.Application.Commands;
using StargateAPI.Application.Queries;

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
        public async Task<IActionResult> GetAstronautDutiesByName([FromRoute] string name)
        {
            try
            {
                var result = await _mediator.Send(new GetPersonByName()
                {
                    Name = name
                });

                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }            
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAstronautDuty([FromBody] CreateAstronautDutyRequest request)
        {
            CreateAstronautDutyCommand astronautDutyCommand = new CreateAstronautDutyCommand(){
                Name = request.Name,
                Rank =  request.Rank,
                DutyTitle = request.DutyTitle,
                DutyStartDate = request.DutyStartDate
            };
            try{
                var result = await _mediator.Send(astronautDutyCommand);
                //log(result);
                return Ok(result);  
            }catch(Exception ex)
            {
                //log(ex);
                return BadRequest(ex.Message);
            }        
        }
    }
}