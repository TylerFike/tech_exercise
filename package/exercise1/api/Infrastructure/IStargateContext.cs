
using Microsoft.EntityFrameworkCore;
using StargateAPI.Application;
using StargateAPI.Infrastructure.Data;

namespace StargateAPI.Infrastructure
{
    public interface IStargateContext
    {
        DbSet<Person> People { get; set; }
        DbSet<AstronautDetail> AstronautDetails { get; set; }
        DbSet<AstronautDuty> AstronautDuties { get; set; }

        Task<PersonDto> GetPersonByName(string name);
        Task<IEnumerable<PersonDto>>GetAllPeople();

        Task<IEnumerable<AstronautDuty>> GetAstronautDuties(int personId);

        Task<AstronautDuty> GetAstronautDuty(int personId);

        Task<AstronautDetail> GetAstronautDetail(int personId);
    }
}
 