using MediatR;
using Moq;

namespace TestAdvisor.Tests.MockBank
{
    public static class MediatorMock
    {
        public static Mock<IMediator> GetMock()
        {
            var mockProvider = new Mock<IMediator>();
                return mockProvider;
        }
    }
}
