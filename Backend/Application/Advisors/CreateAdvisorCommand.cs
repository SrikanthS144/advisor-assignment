using AutoMapper;
using Domain.Data;
using Domain.Models;
using MediatR;
using Application.FluentValidations;
using Newtonsoft.Json;

namespace Application.Advisors
{
    public class CreateAdvisorCommand: IRequest<Advisor>
    {
        public int AdvisorId {  get; set; }
        public string Name {  get; set; }
        public string SIN {  get; set; }
        public string? Address { get; set; }
        public string? Phone {  get; set; }
        public string? HealthStatus {  get; set; }

        public class Handler : IRequestHandler<CreateAdvisorCommand, Advisor>
        {
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly AdvisorContext _context;

            public Handler(AdvisorContext context, IMediator mediator, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _mediator = mediator;
            }

            public async Task<Advisor> Handle(CreateAdvisorCommand command, CancellationToken cancellationToken)
            {
                var _updateValidator = new CreateAdvisorCommandValidator(_context);

                var validationResult = await _updateValidator.ValidateAsync(command, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new Exception(JsonConvert.SerializeObject(validationResult.Errors));
                }

                if (command.AdvisorId != default(int))
                    return await _mediator.Send(_mapper.Map<UpdateAdvisorCommand>(command), cancellationToken);

                var advisor = _mapper.Map<Advisor>(command);

                if(advisor.HealthStatus == null)
                {
                    var random = new Random();
                    int randomHealthStatus = random.Next(0, 100);

                    advisor.HealthStatus = randomHealthStatus.ToString()+"%";
                }
                else
                {
                    advisor.HealthStatus = advisor.HealthStatus + "%";
                }
                
                _context.Advisor.Add(advisor);
                await _context.SaveChangesAsync(cancellationToken);
                return advisor;
            }
        }
    }
}
