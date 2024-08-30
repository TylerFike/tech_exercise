
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

        Task<IEnumerable<PersonDto>> GetPersonByName(string name);
        Task<IEnumerable<PersonDto>>GetAllPeople();
    }
}
 