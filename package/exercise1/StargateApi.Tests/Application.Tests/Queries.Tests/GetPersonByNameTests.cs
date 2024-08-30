using StargateApi.Tests;
using Moq;
using StargateAPI.Infrastructure.Data;
using StargateAPI.Application.Queries;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Application;
namespace Application.Tests.Queries.Tests{
    public class GetPersonByNameTests: BaseTests
    {
        //private readonly StargateContext _context;
        private readonly GetPersonByNameHandler _handler;

        [Test]
        public async Task ShouldReturnPersonDto()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                            .UseInMemoryDatabase(databaseName: "ShouldReturnPersonDto")
                            .Options;
            var dbContext = new StargateContext(options);
            //dbContext.SetUp()
            
            
            //arrange
            var request = new GetPersonByName(){
                Name="test"
            };
            var expectedResult = new PersonDto(){
                Name = "test"
            };
            //var mockContext = new Mock<IStargateContext>();
            var sut = new GetPersonByNameHandler(dbContext);

            await dbContext.People.AddAsync(new Person(){Name = "test", Id = 40});
            var actualResult = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.That(actualResult.Person, Is.EqualTo(expectedResult));

        }

}
}