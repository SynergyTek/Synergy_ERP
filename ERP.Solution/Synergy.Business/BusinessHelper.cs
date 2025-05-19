using Microsoft.Extensions.DependencyInjection;
using Synergy.Business.Implementation;
using Synergy.Business.Interface;

namespace Synergy.Business;

public static class BusinessHelper
{
    public static void Initiate(IServiceCollection services)
    {
        services.Add(new ServiceDescriptor(typeof(IContextBase<,>), typeof(ContextBase<,>), ServiceLifetime.Scoped));
    }
}