using Dapper;
using MediatR;
using StargateAPI.Application;
using StargateAPI.Infrastructure;
using StargateAPI.Infrastructure.Data;

namespace StargateAPI.Application.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {

    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        public readonly StargateContext _context;
        public GetPeopleHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
        {
            var result = new GetPeopleResult();
                     
            var people = await _context.GetAllPeople();

            result.People = people.ToList();

            return result;
        }
    }

    public class GetPeopleResult
    {
        public List<PersonDto> People { get; set; } = new List<PersonDto> { };

    }
}
