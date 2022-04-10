using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PolymorphicModelBinder.Tests.Models;
using PolymorphicModelBinder.Tests.Models.Devices;

namespace PolymorphicModelBinder.Tests
{
    public class PolymorphicModelBinderProviderTests
    {
        [TestCase(typeof(Device))]
        [TestCase(typeof(IDevice))]
        public void Create_ForPolymorphicType_ReturnsPolymorphicBinder(Type modelType)
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddMvc();
            services.AddPolymorphicModelBinder(options =>
            {
                options.Add<IDevice>(builder =>
                {
                    builder.AddFromTypeInValue<Laptop>();
                    builder.AddFromTypeInValue<SmartPhone>();
                });

                options.Add<Device>(builder =>
                {
                    builder.AddFromTypeInValue<Laptop>();
                    builder.AddFromTypeInValue<SmartPhone>();
                });
            });
            var serviceProvider = services.BuildServiceProvider();

            var mvcOptions = serviceProvider.GetService<IOptions<MvcOptions>>();
            var compositeDetailsProvider =
                new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>());
            var metadataProvider = new DefaultModelMetadataProvider(compositeDetailsProvider);
            var modelBinderFactory = new ModelBinderFactory(metadataProvider, mvcOptions, serviceProvider);

            var parameter = new ParameterDescriptor()
            {
                Name = "parameter",
                ParameterType = modelType
            };

            var modelMetadata = metadataProvider.GetMetadataForType(parameter.ParameterType);

            var binderFactoryContext = new ModelBinderFactoryContext()
            {
                BindingInfo = parameter.BindingInfo,
                Metadata = modelMetadata,
                CacheToken = parameter
            };

            // act
            var modelBinder = modelBinderFactory.CreateBinder(binderFactoryContext);

            // Assert
            Assert.AreEqual(typeof(PolymorphicModelBinder), modelBinder?.GetType());
        }

        [TestCase(typeof(List<int>))]
        [TestCase(typeof(int))]
        [TestCase(typeof(string))]
        [TestCase(typeof(Laptop))]
        public void Create_ForNonPolymorphicType_ReturnsDifferentBinder(Type modelType)
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddMvc();
            services.AddPolymorphicModelBinder(_ => { });
            var serviceProvider = services.BuildServiceProvider();

            var mvcOptions = serviceProvider.GetService<IOptions<MvcOptions>>();
            var compositeDetailsProvider =
                new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>());
            var metadataProvider = new DefaultModelMetadataProvider(compositeDetailsProvider);
            var modelBinderFactory = new ModelBinderFactory(metadataProvider, mvcOptions, serviceProvider);

            var parameter = new ParameterDescriptor()
            {
                Name = "parameter",
                ParameterType = modelType
            };

            var modelMetadata = metadataProvider.GetMetadataForType(parameter.ParameterType);

            var binderFactoryContext = new ModelBinderFactoryContext()
            {
                BindingInfo = parameter.BindingInfo,
                Metadata = modelMetadata,
                CacheToken = parameter
            };

            // act
            var modelBinder = modelBinderFactory.CreateBinder(binderFactoryContext);

            // Assert
            Assert.AreNotEqual(typeof(PolymorphicModelBinder), modelBinder?.GetType());
        }
    }
}