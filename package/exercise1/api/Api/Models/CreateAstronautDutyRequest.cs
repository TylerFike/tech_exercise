using MediatR;

namespace StargateAPI.Api.Models{
public class CreateAstronautDutyRequest  : IRequest<CreateAstronautDutyRequest>
{
        public required string Name { get; set; }

        public required string Rank { get; set; }

        public required string DutyTitle { get; set; }

        public DateTime DutyStartDate { get; set; }
}
}