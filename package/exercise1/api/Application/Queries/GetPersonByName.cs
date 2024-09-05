using MediatR;
using StargateAPI.Application;
using StargateAPI.Infrastructure.Data;

namespace StargateAPI.Application.Queries
{
    public class GetPersonByName : IRequest<GetPersonByNameResult>
    {
        public required string Name { get; set; } = string.Empty;
    }

    public class GetPersonByNameHandler : IRequestHandler<GetPersonByName, GetPersonByNameResult>
    {
        private readonly StargateContext _context;
        public GetPersonByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByNameResult> Handle(GetPersonByName request, CancellationToken cancellationToken)
        {
            var result = new GetPersonByNameResult();
            //combine person and person astronaut;

            var personResult = await _context.GetPersonByName(request.Name);

            if(personResult is null) throw new BadHttpRequestException("Person Not Found");

            result.Person = personResult;

            return result;
        }
    }

    public class GetPersonByNameResult
    {
        public PersonDto? Person { get; set; }
    }
}
