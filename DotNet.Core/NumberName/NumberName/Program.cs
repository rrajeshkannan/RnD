using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberName
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            // configure the app configuration, logging, and dependency injection container
            .CreateDefaultBuilder(args)
            // adds everything else required by a typical ASP.NET Core application:
            //   kestrel configuration, and using the Startup class, middleware pipeline
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // use Startup class - for middleware (HTTP request) pipeline
                webBuilder.UseStartup<Startup>();
            });
    }
}
