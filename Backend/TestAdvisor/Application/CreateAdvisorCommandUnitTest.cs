using AutoMapper;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Advisors;
using Domain.Models;
using FluentValidation;
using Xunit.Sdk;

namespace TestAdvisor.Tests.Application
{
    public class CreateAdvisorCommandUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<Domain.Data.AdvisorContext> _context;
        private readonly CreateAdvisorCommand.Handler _handler;
        private readonly Mock<IValidator<CreateAdvisorCommand>> _validator;

        public CreateAdvisorCommandUnitTest()
        {
            _mapper = MockBank.MapperMock.GetMock();
            _mediator = MockBank.MediatorMock.GetMock();
            _context = MockBank.AdvisorContextMock.GetMock();
            _validator = new Mock<IValidator<CreateAdvisorCommand>>();

            _validator
            .Setup(v => v.ValidateAsync(It.IsAny<CreateAdvisorCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            _handler = new CreateAdvisorCommand.Handler(_context.Object, _mediator.Object, _mapper);
        }

        [Fact]
        public async Task CreateNewAdvisor_ValidData_AdvisorCreated()
        {
            // arrange
            var command = new CreateAdvisorCommand
            {
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
        public async Task CreateNewAdvisor_DuplicateAdvisor_ReturnUpdatedLesson()
        {
            // arrange
            var command = new CreateAdvisorCommand
            {
                AdvisorId = 36,
                Name = "Test Lession 1",
                SIN = "987456326",
                Address = "dfrtgsred",
                Phone = "25874103"
            };

            // act
            var result = await _handler.Handle(command, CancellationToken.None);

            // assert
            _mediator.Verify(x => x.Send(It.IsAny<UpdateAdvisorCommand>(), default), Times.Once);
        }

        [Fact]
        public async Task CreateNewAdvisor_Sin_ThrowsException()
        {
            // arrange
            var command = new CreateAdvisorCommand
            {
                Name = "Test Lession 1",
                SIN = "748596123",
                Address = "dfrtgsred",
                Phone = "25874103"
            };

            // act;
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("SIN number already exists.", exception.Message);

        }

        [Fact]
        public async Task CreateNewAdvisor_Phone_ThrowsException()
        {
            // arrange
            var command = new CreateAdvisorCommand
            {
                Name = "Test Lession 1",
                SIN = "748596123",
                Address = "dfrtgsred",
                Phone = "123456"
            };

            // act;
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Phone number must be exactly 8 digits.", exception.Message);
        }

        [Fact]
        public async Task CreateNewAdvisor_Name_ThrowsException()
        {
            // arrange
            var command = new CreateAdvisorCommand
            {
                Name = "",
                SIN = "748596123",
                Address = "dfrtgsred",
                Phone = "12345678"
            };

            // act;
            var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Name is required.", exception.Message);
        }
    }
}
