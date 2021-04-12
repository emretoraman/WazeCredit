using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WazeCredit.Utility.AppSettings;

namespace WazeCredit.Utility
{
    public static class ConfigureAppSettings
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WazeForecastSettings>(configuration.GetSection("WazeForecast"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
            return services;
        }
    }
}
