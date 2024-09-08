namespace Application.Tests.Queries.Tests{
    public class GetAstronautDutiesByNameTests: BaseTests
    {
        [Test]
        public async Task ShouldReturnDutiesList()
        {
            var person = new PersonDto
            {
                Name = "test"
            };
            var dutiesList = new List<AstronautDuty>(){
                new AstronautDuty(){
                    Name = person.Name,
                } as AstronautDuty;
            }

            var dbContext = new Mock<IStargateContext>();
            dbContext.Setup(x => x.GetPersonByName(It.IsAny<string>())).ReturnsAsync(person);
            dbContext.Setup(x => x.GetAstronautDuties(It.IsAny<string>())).ReturnsAsync(person);
            
            //arrange
            var request = new GetPersonByName(){
                Name="test"
            };
            var sut = new GetAstronautDutiesByNameHandler(dbContext.Object);

            var actualResult = await sut.Handle(request, CancellationToken.None);

            //assert
            Assert.That(actualResult.AstronautDuties, Is.EqualTo(dutiesList));

        }

    }
}    