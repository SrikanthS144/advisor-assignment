using Application.Advisors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Newtonsoft.Json;

namespace Api.OData
{
    public class AdvisorController : ODataControllerBase
    {
        [EnableQuery]
        [HttpGet]
        public async Task<IQueryable<Domain.Models.Advisor>> Get() =>
               await Mediator.Send(new GetAdvisorQueryable());

        [HttpGet("{key:int}")]
        public async Task<IActionResult> Get([FromODataUri] int key) =>
            Ok(await Mediator.Send(new GetAdvisorQuery { Key = key }));

        public async Task<IActionResult> Put([FromODataUri] long key, [FromBody] UpdateAdvisorCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (key != command.AdvisorId || !ModelState.IsValid) return BadRequest(ModelState);
                return Ok(await Mediator.Send(command, cancellationToken));
            }
            catch (Exception ex)
            {
                var data = JsonConvert.DeserializeObject<List<FluentValidation.Results.ValidationFailure>>(ex.Message);
                return BadRequest(data);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAdvisorCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var response = await Mediator.Send(command, cancellationToken);
                return Created($"odata/create({response.AdvisorId})", response);
            }
            catch(Exception ex)
            {
                var data = JsonConvert.DeserializeObject<List<FluentValidation.Results.ValidationFailure>>(ex.Message);
                return BadRequest(data);
            }
        }

        public async Task<IActionResult> Delete([FromODataUri] int key, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (key == default(int) || !ModelState.IsValid) return BadRequest(ModelState);
            await Mediator.Send(new DeleteAdvisorCommand { Key = key }, cancellationToken);
            return Ok("Deleted successfully");
        }
    }
}
