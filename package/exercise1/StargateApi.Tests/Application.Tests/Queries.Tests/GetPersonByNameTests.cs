using StargateApi.Tests;
using Moq;
using StargateAPI.Infrastructure;
using StargateAPI.Application.Queries;
using StargateAPI.Application;
namespace Application.Tests.Queries.Tests{
    public class GetPersonByNameTests: BaseTests
    {
        //private readonly StargateContext _context;
        //private readonly GetPersonByNameHandler _handler;

        [Test]
        public async Task ShouldReturnPersonDto()
        {
            var person = new PersonDto
            {
                Name = "test"
            };
            var dbContext = new Mock<IStargateContext>();
            dbContext.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(person);
            
            //arrange
            var request = new GetPersonByName(){
                Name="test"
            };
            var sut = new GetPersonByNameHandler(dbContext.Object);

            var actualResult = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.That(actualResult.Person, Is.EqualTo(person));

        }

}
}