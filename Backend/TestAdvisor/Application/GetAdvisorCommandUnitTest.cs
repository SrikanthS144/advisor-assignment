using Application.Advisors;
using AutoMapper;
using Domain.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestAdvisor.Tests.MockBank;

namespace TestAdvisor.Tests.Application
{
    public class GetAdvisorCommandUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediator;
        private readonly AdvisorContext _context;
        private readonly GetAdvisorQueryable.Handler _handler;
        private readonly Mock<IValidator<GetAdvisorQueryable>> _validator;

        public GetAdvisorCommandUnitTest()
        {
            _context = AdvisorContextMock.GetMock().Object;
        }

        [Fact]
        public async Task Handle_GetAvisorList_Successfully()
        {
            // arrange
            var command = new GetAdvisorQueryable();
            var handler = new GetAdvisorQueryable.Handler(_context);

            // act
            var result = await handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ListAdvisors_ReturnsEmptyList_WhenNoAdvisorsExist()
        {
            // arrange
            var command = new GetAdvisorQueryable();
            var handler = new GetAdvisorQueryable.Handler(_context);

            // act
            var result = await handler.Handle(command, CancellationToken.None);
            var totalAdvisorFound = result.Count();

            // assert
            Assert.False(totalAdvisorFound == 0);
        }

        [Fact]
        public async Task Handle_GetAvisorSINExist_Successfully()
        {
            var existingSIN = "748596123";

            var expectedAdvisor = new Domain.Models.Advisor
            {
                AdvisorId = 35,
                Name = "jay",
                Sin = existingSIN,
                Address = "asdasdaas nasd asd asdasd asd asd as das",
                Phone = "12345678",
                Created = new DateTime(),
                CreatedBy = 0,
                Updated = new DateTime(),
                UpdatedBy = 0
            };

            // arrange
            var command = new GetAdvisorQueryable();
            var handler = new GetAdvisorQueryable.Handler(_context);

            // act
            var result = await handler.Handle(command, CancellationToken.None);
            var existingSINFound = await result.FirstOrDefaultAsync(a => a.Sin == existingSIN);

            // assert
            Assert.NotNull(existingSINFound);
            Assert.Equal(expectedAdvisor.Name, existingSINFound.Name);
            Assert.Equal(expectedAdvisor.Sin, existingSINFound.Sin);
            Assert.Equal(expectedAdvisor.Address, existingSINFound.Address);
            Assert.Equal(expectedAdvisor.Phone, existingSINFound.Phone);
            Assert.Equal(expectedAdvisor.HealthStatus, existingSINFound.HealthStatus);
        }

        [Fact]
        public async Task Handle_GetAvisorSINNotExist_Successfully()
        {
            var existingSIN = "987654321";

            // arrange
            var command = new GetAdvisorQueryable();
            var handler = new GetAdvisorQueryable.Handler(_context);

            // act
            var result = await handler.Handle(command, CancellationToken.None);
            var existingSINFound = await result.FirstOrDefaultAsync(a => a.Sin == existingSIN);

            // assert
            Assert.Null(existingSINFound);
        }
    }
}
