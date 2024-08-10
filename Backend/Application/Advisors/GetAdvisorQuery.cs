using Domain.Data;
using Domain.Models;
using MediatR;

namespace Application.Advisors
{
    public class GetAdvisorQuery : IRequest<Advisor>
    {
        public int Key { get; set; }
        public class Handler : IRequestHandler<GetAdvisorQuery, Advisor>
        {
            private readonly AdvisorContext _context;

            public Handler(AdvisorContext context)
            {
                _context = context;
            }

            public async Task<Advisor> Handle(GetAdvisorQuery query, CancellationToken cancellationToken)
            {
                var entity = await _context.Advisor.FindAsync(query.Key);

                return entity;
            }
        }
    }
}
