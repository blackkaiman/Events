using System.Linq;
using EvoEvents.Data.Models.ApplicationVersions;
using EvoEvents.Business.Versions.Models;

namespace EvoEvents.Business.Versions
{
    public static class VersionExtensions
    {
        public static IQueryable<VersionCode> ToVersionCode(this IQueryable<ApplicationVersion> query)
        {
            return query.Select(q => new VersionCode
            {
                Version = q.Version
            });
        }
    }
}
