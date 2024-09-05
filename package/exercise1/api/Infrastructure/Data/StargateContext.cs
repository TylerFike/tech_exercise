using Dapper;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Application;
namespace StargateAPI.Infrastructure.Data
{
    public class StargateContext : DbContext, IStargateContext
    {
        //public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<Person> People { get; set; }
        public DbSet<AstronautDetail> AstronautDetails { get; set; }
        public DbSet<AstronautDuty> AstronautDuties { get; set; }

        public StargateContext(DbContextOptions<StargateContext> options)
        : base(options)
        {
        }

        public async Task<PersonDto> GetPersonByName(string name){
            var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE @name = a.Name";
            //var people = SqlQuery<PersonDto>(query, name);
            using(var connection = Database.GetDbConnection()){
                return await connection.QueryFirstOrDefaultAsync<PersonDto>(query, new { name});
            }
        }

        public async Task<IEnumerable<PersonDto>>GetAllPeople(){
            var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id";
            using(var connection = Database.GetDbConnection()){
                return await connection.QueryAsync<PersonDto>(query);
            }
        }

        public async Task<IEnumerable<AstronautDuty>> GetAstronautDuties(int personId){
            var query = $"SELECT * FROM [AstronautDuty] WHERE @personId = PersonId Order By DutyStartDate Desc";
            using(var connection = Database.GetDbConnection()){
                return await connection.QueryAsync<AstronautDuty>(query, new {personId});
            }
        }

        public async Task<AstronautDuty> GetAstronautDuty(int personId){
            var query = $"SELECT * FROM [AstronautDuty] WHERE @personId = PersonId Order By DutyStartDate Desc";
            using(var connection = Database.GetDbConnection()){
                return await connection.QueryFirstOrDefaultAsync<AstronautDuty>(query, new {personId});
            }
        }

        public async Task<AstronautDetail> GetAstronautDetail(int personId){
            var  query = $"SELECT * FROM [AstronautDetail] WHERE @personId = PersonId";
            using(var connection = Database.GetDbConnection()){
                return await connection.QueryFirstOrDefaultAsync<AstronautDetail>(query, new {personId});
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StargateContext).Assembly);

            //SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            //add seed data
            modelBuilder.Entity<Person>()
                .HasData(
                    new Person
                    {
                        Id = 1,
                        Name = "John Doe"
                    },
                    new Person
                    {
                        Id = 2,
                        Name = "Jane Doe"
                    }
                );

            modelBuilder.Entity<AstronautDetail>()
                .HasData(
                    new AstronautDetail
                    {
                        Id = 1,
                        PersonId = 1,
                        CurrentRank = "1LT",
                        CurrentDutyTitle = "Commander",
                        CareerStartDate = DateTime.Now
                    }
                );

            modelBuilder.Entity<AstronautDuty>()
                .HasData(
                    new AstronautDuty
                    {
                        Id = 1,
                        PersonId = 1,
                        DutyStartDate = DateTime.Now,
                        DutyTitle = "Commander",
                        Rank = "1LT"
                    }
                );
        }
    }
}
