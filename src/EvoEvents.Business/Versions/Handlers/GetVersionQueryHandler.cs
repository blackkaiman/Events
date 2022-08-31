using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EvoEvents.Business.Versions.Models;
using EvoEvents.Business.Versions.Queries;
using EvoEvents.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvoEvents.Business.Versions.Handlers
{
    public class GetVersionQueryHandler : IRequestHandler<GetVersionQuery, VersionCode>
    {
        private readonly EvoEventsContext _context;

        public GetVersionQueryHandler(EvoEventsContext context)
        {
            _context = context;
        }

        public async Task<VersionCode> Handle(GetVersionQuery request, CancellationToken cancellationToken)
        {
            return await _context.ApplicationVersions
                .Where(v => v.Name == request.Name)
                .ToVersionCode()
                .FirstOrDefaultAsync();
        }
    }
}
