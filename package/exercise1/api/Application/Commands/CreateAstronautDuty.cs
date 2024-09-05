using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Infrastructure.Data;

namespace StargateAPI.Application.Commands
{
    public class CreateAstronautDutyCommand : IRequest<CreateAstronautDutyResult>
    {
        public required string Name { get; set; }

        public required string Rank { get; set; }

        public required string DutyTitle { get; set; }

        public DateTime DutyStartDate { get; set; }
    }

    public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDutyCommand>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyPreProcessor(StargateContext context)
        {
            _context = context;
        }

        public Task Process(CreateAstronautDutyCommand request, CancellationToken cancellationToken)
        {
            var person = _context.People.AsNoTracking().FirstOrDefault(z => z.Name == request.Name);
            
            if(person is null)throw new BadHttpRequestException("Person Not Found");
            
            var verifyNoPreviousDuty = _context.AstronautDuties.FirstOrDefault(z => z.DutyTitle == request.DutyTitle && z.DutyStartDate == request.DutyStartDate);

            if (verifyNoPreviousDuty is not null) throw new BadHttpRequestException("Duplicate Duty");

            return Task.CompletedTask;
        }
    }

    public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDutyCommand, CreateAstronautDutyResult>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDutyCommand request, CancellationToken cancellationToken)
        {

            var person = await _context.GetPersonByName(request.Name);

            var astronautDetail = await _context.GetAstronautDetail(person.PersonId);


            if (astronautDetail == null)
            {
                astronautDetail = new AstronautDetail(){
                    PersonId = person.PersonId,
                    CurrentDutyTitle = request.DutyTitle,
                    CurrentRank = request.Rank,
                    CareerStartDate = request.DutyStartDate.Date,
                };
                if (request.DutyTitle == "RETIRED")
                {
                    astronautDetail.CareerEndDate = request.DutyStartDate.Date;
                }

                await _context.AstronautDetails.AddAsync(astronautDetail);

            }
            else
            {
                astronautDetail.CurrentDutyTitle = request.DutyTitle;
                astronautDetail.CurrentRank = request.Rank;
                if (request.DutyTitle == "RETIRED")
                {
                    astronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
                }
                _context.AstronautDetails.Update(astronautDetail);
            }

            //Update current duty
            var astronautDuty = await _context.GetAstronautDuty(person.PersonId);

            if (astronautDuty != null)
            {
                astronautDuty.DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
                _context.AstronautDuties.Update(astronautDuty);
            }

            var newAstronautDuty = new AstronautDuty()
            {
                PersonId = person.PersonId,
                Rank = request.Rank,
                DutyTitle = request.DutyTitle,
                DutyStartDate = request.DutyStartDate.Date,
                DutyEndDate = null
            };

            await _context.AstronautDuties.AddAsync(newAstronautDuty);

            await _context.SaveChangesAsync();

            return new CreateAstronautDutyResult()
            {
                Id = newAstronautDuty.Id
            };
        }
    }

    public class CreateAstronautDutyResult 
    {
        public int? Id { get; set; }
    }
}
