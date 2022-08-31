using EvoEvents.Business.Versions.Models;
using MediatR;

namespace EvoEvents.Business.Versions.Queries
{
    public class GetVersionQuery : IRequest<VersionCode>
    {
        public string Name { get; set; }
    }
}
