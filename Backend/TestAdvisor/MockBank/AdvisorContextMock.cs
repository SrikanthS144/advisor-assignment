using Moq;
using Domain.Data;

namespace TestAdvisor.Tests.MockBank
{
  public static class AdvisorContextMock
  {
    public static Mock<AdvisorContext> GetMock()
    {
      var mockProvider = new Mock<AdvisorContext>();

            var advisorMockSet = ContextMockHelper.GetAdvisorMockSet();
            mockProvider.Setup(x => x.Advisor).Returns(advisorMockSet.Object);

            return mockProvider;
    }

  }
}
