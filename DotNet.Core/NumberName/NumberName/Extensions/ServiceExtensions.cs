using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NumberName.Extensions
{
    public static class ServiceExtensions
    {
        // CORS (Cross-Origin Resource Sharing) gives rights to the user to access resources from the server on a different domain.
        // Since we are going to use Angular as a client-side on a different domain than the server’s domain, configuring CORS is mandatory
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder 
                    => builder
                    // AllowAnyOrigin - allows requests from any source
                    // To be more restrictive:
                    //   We could use WithOrigins to allow requests only from specified source, 
                    //   for e.g. WithOrigins("http://www.something.com")
                    .AllowAnyOrigin()
                    // AllowAnyMethod - allows all HTTP methods
                    // To be more restrictive:
                    //   we can use WithMethods to allow only specified HTTP methods
                    //   for e.g. WithMethods("POST", "GET")
                    .AllowAnyMethod()
                    // AllowAnyHeader - allows any header having any meta info
                    // To restrict:
                    //   we can use WithHeaders to allow only specified headers
                    //   for e.g. WithHeaders("accept", "content-type")
                    .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
            });
        }
    }
}