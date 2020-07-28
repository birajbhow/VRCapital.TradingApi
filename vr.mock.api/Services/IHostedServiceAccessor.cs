using Microsoft.Extensions.Hosting;

namespace vr.mock.api.Services
{
    public interface IHostedServiceAccessor<out T> where T : IHostedService
    {
        T Service { get; }
    }
}