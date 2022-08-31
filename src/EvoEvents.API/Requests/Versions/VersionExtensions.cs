using EvoEvents.Business.Versions.Queries;

namespace EvoEvents.API.Requests.Versions
{
    public static class VersionExtensions
    {
        public static GetVersionQuery ToQuery(this GetVersionRequest request)
        {
            return new GetVersionQuery
            {
                Name = request.Name
            };
        }
    }
}
