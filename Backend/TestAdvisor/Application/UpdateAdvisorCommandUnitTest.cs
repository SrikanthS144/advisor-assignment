using Moq;
using Application.Advisors;
using AutoMapper;
using MediatR;


namespace TestAdvisor.Tests.Application
{
    public class UpdateAdvisorCommandUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<Domain.Data.AdvisorContext> _context;
        private readonly UpdateAdvisorCommand.Handler _handler;

        public UpdateAdvisorCommandUnitTest()
        {
            _mapper = MockBank.MapperMock.GetMock();
            _context = MockBank.AdvisorContextMock.GetMock();
            _mediator = MockBank.MediatorMock.GetMock();
            _handler = new UpdateAdvisorCommand.Handler(_context.Object, _mapper);
        }

        [Fact]
        public async Task Handle_UpdateAdvisor_Successfully()
        {
            // arrange
            var command = new UpdateAdvisorCommand
            {
                AdvisorId= 35,
                Name = "Test Advisor",
                SIN = "987456328",
                Address = "dfrtgsred",
                Phone = "25874103"
            };

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.True(result.Name == "Test Advisor");
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_AdvisorNotFound_ThrowsNotFoundException()
        {
            // arrange
            var command = new UpdateAdvisorCommand { AdvisorId = -1 };

            // act & assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("AdvisorId must be greater than zero.", exception.Message);
        }

        [Fact]
        public async Task UpdateAdvisor_ValidData_AdvisorCreated()
        {
            // arrange
            var command = new UpdateAdvisorCommand
            {
                AdvisorId = 36,
                Name = "Test Lession 1",
                SIN = "987456328",
                Address = "dfrtgsred",
                Phone = "25874103"
            };

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            Assert.NotNull(result);
            _context.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task UpdateAdvisor_Sin_ThrowsException()
        {
            // arrange
            var command = new UpdateAdvisorCommand
            {
                AdvisorId = 36,
                Name = "Test Lession 1",
                SIN = "748596123",
                Address = "dfrtgsred",
                Phone = "25874103"
            };

            // act & assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal("SIN number is already exist", exception.Message);
        }

        [Fact]
        public async Task UpdateAdvisor_Phone_ThrowsException()
        {
            // arrange
            var command = new UpdateAdvisorCommand
            {
                AdvisorId = 36,
                Name = "Test Lession 1",
                SIN = "748896123",
                Address = "dfrtgsred",
                Phone = "698523"
            };

            // act & assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Phone number must be exactly 8 digits.", exception.Message);
        }

        [Fact]
        public async Task UpdateAdvisor_Name_ThrowsException()
        {
            // arrange
            var command = new UpdateAdvisorCommand
            {
                AdvisorId = 36,
                Name = "",
                SIN = "748596123",
                Address = "dfrtgsred",
                Phone = "12345678"
            };

            // act & assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Name is required.", exception.Message);
        }
    }
}
