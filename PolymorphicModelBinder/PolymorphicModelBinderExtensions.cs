using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PolymorphicModelBinder.Providers;

namespace PolymorphicModelBinder
{
    public static class PolymorphicModelBinderExtensions
    {
        public static IServiceCollection AddPolymorphicModelBinder(this IServiceCollection services, Action<PolymorphicModelBinderOptions> options)
        {
            services.AddSingleton<IValidateOptions<PolymorphicModelBinderOptions>, PolymorphicModelBinderOptionsValidator>();
            services.AddSingleton<IPolymorphicBindableModelBinderProvider, PolymorphicBindableModelBinderProvider>();
        
            services
                .AddOptions<PolymorphicModelBinderOptions>()
                .Configure(modelBinderOptions =>
                {
                    options?.Invoke(modelBinderOptions);
                })
                .ValidateOnStart();
        
            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.ModelBinderProviders.Insert(0, new PolymorphicModelBinderProvider());
            });

            return services;
        }
    }
}