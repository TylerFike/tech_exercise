using StargateApi.Tests;
using Moq;
using StargateAPI.Infrastructure;
using StargateAPI.Application.Queries;
using StargateAPI.Application;
using Microsoft.AspNetCore.Http;
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

        [Test]
        public async Task ShouldThrowExceptionWhenPersonIsNull()
        {
            var person = new PersonDto
            {
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
            Assert.ThrowsAsync<BadHttpRequestException>(() => throw new BadHttpRequestException("Person Not Found"));
            Assert.That(actualResult.Person, Is.EqualTo(person));
        }

}
}