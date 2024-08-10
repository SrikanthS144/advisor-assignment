using MediatR;
using Moq;
using Application.Advisors;

namespace TestAdvisor.Tests.MockBank
{
  public static class MediatorMock
  {
    public static Mock<IMediator> GetMock()
    {
      var mockProvider = new Mock<IMediator>();

           /* mockProvider
            .Setup(x => x.Send(It.IsAny<UpdateAdvisorCommand>(), default))
            .Returns(Task.FromResult(new Mediator()));*/

            /*mockProvider
            .Setup(x => x.Send(It.IsAny<UpdateReferralSourceSpendingLimitCommand>(), default))
            .Returns(Task.FromResult(new ReferralSourceSpendingLimit()));

            mockProvider
            .Setup(x => x.Send(It.IsAny<UpdatePayorCommand>(), default))
            .Returns(Task.FromResult(new Payor()));

            mockProvider
            .Setup(x => x.Send(It.IsAny<UpdateDiagnosisCommand>(), default))
            .Returns(Task.FromResult(new Diagnosis()));*/

            return mockProvider;
    }
  }
}
