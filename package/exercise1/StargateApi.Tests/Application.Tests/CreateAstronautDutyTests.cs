using StargateApi.Tests;
using Moq;
using StargateAPI.Infrastructure.Data;
using StargateAPI.Application.Commands;
namespace Application.Tests{
    public class CreateAstronautDutyHandlerTests : BaseTests
    {
        [Test]
        public void ShouldThrowExceptionWhenPersonIsNotNull()
        {
            
            //arrange
            //var contextMock = new Mock<StargateContext>();
            //var _sut = new CreateAstronautDutyPreProcessor(contextMock.Object);
            //var request = new CreateAstronautDutyCommand(){
            //    Name = "test",
           //     Rank = "test",
            //    DutyTitle = "test",
            //    DutyStartDate = new DateTime(),
            //};
            //contextMock.Object.People.FirstOrDefault(z => z.Name == request.Name).Returns(new Person { Name = "test"});
            //act
            //var result = () => _sut.Process(request, new CancellationToken());

            //assert
            //result.Should().ThrowAsync<BadHttpRequestException>();

        }

}
}
