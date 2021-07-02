using System.Threading;
using System.Threading.Tasks;
using Hermes.Data;
using Hermes.Shared;
using MediatR;

namespace Hermes.Features
{
    public class HealthHandler: IRequestHandler<HealthQuery, Result<string>>
    {
        public Task<Result<string>> Handle(HealthQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Ok("S'All Good!"));
        }
    }
}