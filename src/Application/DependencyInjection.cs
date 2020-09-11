using ConsoleTemplate.$safeprojectname$.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTemplate.$safeprojectname$
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add DI services
            services.AddTransient<IPrinterService, PrinterService>();

            return services;
        }
    }
}
