using Microsoft.Extensions.DependencyInjection;
using System;
using WazeCredit.Data.Repository;
using WazeCredit.Data.Repository.Interfaces;
using WazeCredit.Models;
using WazeCredit.Services;
using WazeCredit.Services.Lifetime;

namespace WazeCredit.Utility
{
    public static class ConfigureDiServices
    {
        public static IServiceCollection AddDiServices(this IServiceCollection services)
        {
            services.AddTransient<IMarketForecaster, MarketForecaster>();

            services.AddTransient<TransientService>();
            services.AddScoped<ScopedService>();
            services.AddSingleton<SingletonService>();

            services.AddScoped<IValidationChecker, AddressValidationChecker>();
            services.AddScoped<IValidationChecker, CreditValidationChecker>();
            services.AddScoped<ICreditValidator, CreditValidator>();

            services.AddScoped<CreditApprovedHigh>();
            services.AddScoped<CreditApprovedLow>();

            services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(serviceProvider => range =>
            {
                return range switch
                {
                    CreditApprovedEnum.Low => serviceProvider.GetService<CreditApprovedLow>(),
                    CreditApprovedEnum.High => serviceProvider.GetService<CreditApprovedHigh>(),
                    _ => serviceProvider.GetService<CreditApprovedLow>(),
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
