//using Api.OData;
//using Application.Advisors;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestAdvisor.Controller
//{
//    public class AdvisorControllerTest
//    {
//        [Fact]
//        public async void Put_ValidCommand_ReturnsUpdatedResult()
//        {
//            /*var dataStore = FakeItEasy.A.Fake<Domain.Models.Advisor>();
//            var controller = new AdvisorController();

//            var command = new CreateAdvisorCommand() { Name = "Test" };

//            var a = await controller.Post(command);*/

//            var advisorContextMock = new Mock<Domain.Data.AdvisorContext>();
//            advisorContextMock.Setup<DbSet<Domain.Models.Advisor>>(x => x.Advisor)
//                .ReturnsDbSet(TestDataHelper.GetFakeEmployeeList());
//            //Act
//            AdvisorController advisorController = new(advisorContextMock.Object);
//            var employees = (await advisorController.GetAdvisors()).Value;
//            //Assert
//            Assert.NotNull(employees);
//            Assert.Equal(2, employees.Count());
//        }
//    }

//    private static List<Domain.Models.Advisor> GetFakeEmployeeList()
//    {
//        return new List<Domain.Models.Advisor>()
//    {
       
//    };
//    }
//}
