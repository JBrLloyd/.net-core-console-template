using Domain.Entities;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleTemplate.$safeprojectname$.Services
{

    public class PrinterService : IPrinterService
    {
        private readonly ILogger<PrinterService> _logger;
        private readonly IConfiguration _configuration;

        public PrinterService(ILogger<PrinterService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void ConsoleOut()
        {
            _logger.LogInformation("Running Console Printing");
            var user = new User
            {
                FirstName = "Jackson",
                LastName = "Lloyd"
            };
            Console.WriteLine($"Hello {user.FirstName} {user.LastName}!");
        }
    }
}