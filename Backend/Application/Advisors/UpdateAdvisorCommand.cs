using Application.FluentValidations;
using AutoMapper;
using Domain.Data;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Advisors
{
    public class UpdateAdvisorCommand: IRequest<Advisor>
    {
        public int AdvisorId { get; set; }
        public string Name { get; set; }
        public string SIN { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? HealthStatus { get; set; }

        public class Handler : IRequestHandler<UpdateAdvisorCommand, Advisor>
        {
            private readonly IMapper _mapper;
            private readonly AdvisorContext _context;
            public Handler(AdvisorContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Advisor> Handle(UpdateAdvisorCommand command, CancellationToken cancellationToken)
            {
                var _updateValidator = new UpdateAdvisorCommandValidator();

                var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new Exception(JsonConvert.SerializeObject(validationResult.Errors));
                }

                var entity = await _context.Advisor.SingleOrDefaultAsync(x => x.AdvisorId == command.AdvisorId, cancellationToken);

                if(entity == null){
                    throw new Exception("EntityId is not exist");
                }

                var advisor = _mapper.Map<Advisor>(command);

                if (entity.Sin != command.SIN)
                {
                    var isExist = await _context.Advisor.FirstOrDefaultAsync(a => a.Sin == advisor.Sin, cancellationToken);

                    if (isExist != null)
                    {
                        throw new Exception("SIN number is already exist");
                    }
                }

                entity.Name = command.Name;
                entity.Sin = command.SIN;
                entity.Address = command.Address;
                entity.Phone = command.Phone;

                if (command.HealthStatus != null)
                {
                    entity.HealthStatus = command.HealthStatus +"%";
                }

                await _context.SaveChangesAsync(cancellationToken);
                return entity;
            }
        }
    }
}
