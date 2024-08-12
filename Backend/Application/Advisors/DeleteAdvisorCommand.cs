using Application.Exceptions;
using Domain.Data;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Advisors
{
    public class DeleteAdvisorCommand :IRequest<Advisor>
    {
        public int Key { get; set; }
        public class Handler : IRequestHandler<DeleteAdvisorCommand, Advisor>
        {
            private readonly AdvisorContext _context;

            public Handler(AdvisorContext context)
            {
                _context = context;
            }

            public async Task<Advisor> Handle(DeleteAdvisorCommand command, CancellationToken cancellationToken)
            {
                var entity = await _context.Advisor.SingleOrDefaultAsync(x => x.AdvisorId == command.Key, cancellationToken);
                if (entity == null)
                    throw new NotFoundException(nameof(Advisor), command.Key);

                _context.Advisor.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
    }
}
