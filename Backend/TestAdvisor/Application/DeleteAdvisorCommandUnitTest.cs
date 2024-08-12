using Moq;
using Application.Advisors;
using Application.Exceptions;

namespace TestAdvisor.Tests.Application
{
    public class DeleteAdvisorCommandUnitTest
    {
        private readonly Mock<Domain.Data.AdvisorContext> _context;

        public DeleteAdvisorCommandUnitTest()
        {
            _context = MockBank.AdvisorContextMock.GetMock();
        }

        [Fact]
        public async Task Handle_DeleteLesson_Successfully()
        {
            // arrange
            var command = new DeleteAdvisorCommand { Key = 35 };
            var handler = new DeleteAdvisorCommand.Handler(_context.Object);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(result.Deleted);
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task handle_advisornotfound_throwsnotfoundexception()
        {
            // arrange
            var command = new DeleteAdvisorCommand { Key = 39 };
            var handler = new DeleteAdvisorCommand.Handler(_context.Object);

            // act & assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
