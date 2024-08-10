using Domain.Data;
using Domain.Models;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace Application.Advisors
{
    public class GetAdvisorQueryable: IRequest<IQueryable<Advisor>>
    {
        public class Handler : IRequestHandler<GetAdvisorQueryable, IQueryable<Advisor>>
        {
            public readonly AdvisorContext _context;
            public Handler(AdvisorContext context)
            {
                _context = context;
            }

            public Task<IQueryable<Advisor>> Handle(GetAdvisorQueryable query, CancellationToken cancellationToken)
            {
                return Task.FromResult((IQueryable<Advisor>)_context.Advisor);
            }
        }
    }
}
