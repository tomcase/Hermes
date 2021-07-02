using System.Threading;
using System.Threading.Tasks;
using Hermes.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hermes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HealthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var result = await _mediator.Send(new HealthQuery(), token);
            if (result.IsFailure)
                return BadRequest();
            return Ok(result.Value);
        }
    }
}
