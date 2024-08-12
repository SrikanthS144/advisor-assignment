using Microsoft.EntityFrameworkCore;
using Moq;

namespace TestAdvisor.Tests.MockBank
{
    public static class ContextMockHelper
    {
        public static Mock<DbSet<Domain.Models.Advisor>> GetAdvisorMockSet()
        {
            var data = new List<Domain.Models.Advisor>
            {
                new()
                {
                    AdvisorId= 35,
                    Name = "jay",
                    Sin = "748596123",
                    Address = "asdasdaas nasd asd asdasd asd asd as das",
                    Phone = "12345678",
                    Created = new DateTime(),
                    CreatedBy = 0,
                    Updated = new DateTime(),
                    UpdatedBy = 0
                },
                new()
                {
                    AdvisorId= 36,
                    Name = "Test",
                    Sin = "748596125",
                    Address = "asdasdaas nasd asd asdasd asd asd as das",
                    Phone = "12345678",
                    Created = new DateTime(),
                    CreatedBy = 0,
                    Updated = new DateTime(),
                    UpdatedBy = 0
                }
            };

            var lessonMockSet = CreateDbSetMock(data.AsQueryable());
            lessonMockSet
            .Setup(m => m.AddAsync(It.IsAny<Domain.Models.Advisor>(), default))
              .Callback<Domain.Models.Advisor, CancellationToken>((s, _) =>
              {
                  s.AdvisorId = data.Count + 1;
                  data.Add(s);
              });

            lessonMockSet.Setup(m => m.Remove(It.IsAny<Domain.Models.Advisor>()))
              .Callback<Domain.Models.Advisor>(s =>
              {
                  data.Remove(data.Find(t => t.AdvisorId == s.AdvisorId));
              });

            lessonMockSet
              .Setup(m => m.FindAsync(It.IsAny<object[]>()))
              .Returns((object[] r) =>
              {
                  var advisor = lessonMockSet.Object
            .FirstOrDefaultAsync(b => b.AdvisorId == (int)r[0]);
                  return new ValueTask<Domain.Models.Advisor>(advisor);
              });

            return lessonMockSet;
        }
        
        private static Mock<DbSet<T>> CreateDbSetMock<T>(IQueryable<T> items) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IAsyncEnumerable<T>>()
              .Setup(x => x.GetAsyncEnumerator(default))
              .Returns(new TestAsyncEnumerator<T>(items.GetEnumerator()));
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.Provider)
              .Returns(new TestAsyncQueryProvider<T>(items.Provider));
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.Expression).Returns(items.Expression);
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.ElementType).Returns(items.ElementType);
            dbSetMock.As<IQueryable<T>>()
              .Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            return dbSetMock;
        }
    }
}
