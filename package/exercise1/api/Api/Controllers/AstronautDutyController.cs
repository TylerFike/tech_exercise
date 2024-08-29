using MediatR;
using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Queries;
using StargateAPI.Api.Models;
using System.Net;
using StargateAPI.Business.Data;
using StargateAPI.Business.Commands;
using Microsoft.EntityFrameworkCore.Query.Internal;

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
                return NotFound();
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
                
                return Ok(result);  
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }        
        }
    }
}