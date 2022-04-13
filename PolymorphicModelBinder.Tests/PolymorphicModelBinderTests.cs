using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using PolymorphicModelBinder.Entries.TypeInValue;
using PolymorphicModelBinder.Tests.Models.Devices;
using PolymorphicModelBinder.Tests.Models.Pets;

namespace PolymorphicModelBinder.Tests
{
    public class PolymorphicModelBinderTests
    {
    
        public static (FormCollection, IPet)[] DiscriminatorTestCases()
        {
            return new (FormCollection formCollection, IPet expected)[]
            {
                (
                    new FormCollection(new Dictionary<string, StringValues>()
                    {
                        { "Discriminator", "Cat" },
                        { nameof(IPet.Name), "Garfield" }
                    }),
                    new Cat()
                    {
                        Name = "Garfield"
                    }
                ),
                (
                    new FormCollection(new Dictionary<string, StringValues>()
                    {
                        { "Discriminator", "Dog" },
                        { nameof(IPet.Name), "Noeska" }
                    }),
                    new Dog()
                    {
                        Name = "Noeska"
                    }
                ),
            };
        }
    
        [Test]
        public async Task DiscriminatorTests([ValueSource(nameof(DiscriminatorTestCases))] (FormCollection formCollection, IPet expected) testCase)
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddMvc();
            services.AddPolymorphicModelBinder(options =>
            {
                options.Add<IPet>(builder =>
                {
                    builder.AddFromDiscriminator<Dog>();
                    builder.AddFromDiscriminator<Cat>();
                });
            });
            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetService<IOptions<MvcOptions>>();
            var compositeDetailsProvider =
                new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>());
            var metadataProvider = new DefaultModelMetadataProvider(compositeDetailsProvider);
            var modelBinderFactory = new ModelBinderFactory(metadataProvider, options, serviceProvider);

            var parameterBinder = new ParameterBinder(
                metadataProvider,
                modelBinderFactory,
                new Mock<IObjectModelValidator>().Object,
                options,
                NullLoggerFactory.Instance);

            var parameter = new ParameterDescriptor()
            {
                Name = "parameter",
                ParameterType = typeof(IPet)
            };

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices =
                        serviceProvider
                },
                RouteData = new RouteData()
            };

            var modelMetadata = metadataProvider.GetMetadataForType(parameter.ParameterType);

            var valueProvider =
                new FormValueProvider(BindingSource.Form, testCase.formCollection, CultureInfo.CurrentCulture);

            var modelBinder = modelBinderFactory.CreateBinder(new ModelBinderFactoryContext()
            {
                BindingInfo = parameter.BindingInfo,
                Metadata = modelMetadata,
                CacheToken = parameter
            });

            // Act
            var modelBindingResult = await parameterBinder.BindModelAsync(
                controllerContext,
                modelBinder,
                valueProvider,
                parameter,
                modelMetadata,
                value: null);

            // Assert
            Assert.True(modelBindingResult.IsModelSet);
            modelBindingResult.Model.Should().BeEquivalentTo(testCase.expected);
        }
    
    
        public static (FormCollection, Device)[] TypeInValueTestCases()
        {
            return new (FormCollection formCollection, Device expected)[]
            {
                (
                    new FormCollection(new Dictionary<string, StringValues>()
                    {
                        { TypeInValuePolymorphicBindable.FieldName, typeof(Laptop).AssemblyQualifiedName },
                        { nameof(Laptop.Brand), "Samsung" },
                        { nameof(Laptop.CpuIndex), "80" }
                    }),
                    new Laptop()
                    {
                        Brand = "Samsung",
                        CpuIndex = "80"
                    }
                ),
                (
                    new FormCollection(new Dictionary<string, StringValues>()
                    {
                        { TypeInValuePolymorphicBindable.FieldName, typeof(Laptop).AssemblyQualifiedName },
                        { nameof(Laptop.Brand), "Google" },
                        { nameof(Laptop.CpuIndex), "10inch" }
                    }),
                    new SmartPhone()
                    {
                        Brand = "Google",
                        ScreenSize = "10inch"
                    }
                )
            };
        }

        [Test]
        public async Task TypeInValueTests([ValueSource(nameof(TypeInValueTestCases))] (FormCollection formCollection, Device expected) testCase)
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddMvc();
            services.AddPolymorphicModelBinder(options =>
            {
                options.Add<Device>(builder =>
                {
                    builder.AddFromTypeInValue<Laptop>();
                    builder.AddFromTypeInValue<SmartPhone>();
                });
            });
            var serviceProvider = services.BuildServiceProvider();

            var options = serviceProvider.GetService<IOptions<MvcOptions>>();
            var compositeDetailsProvider =
                new DefaultCompositeMetadataDetailsProvider(new List<IMetadataDetailsProvider>());
            var metadataProvider = new DefaultModelMetadataProvider(compositeDetailsProvider);
            var modelBinderFactory = new ModelBinderFactory(metadataProvider, options, serviceProvider);

            var parameterBinder = new ParameterBinder(
                metadataProvider,
                modelBinderFactory,
                new Mock<IObjectModelValidator>().Object,
                options,
                NullLoggerFactory.Instance);

            var parameter = new ParameterDescriptor()
            {
                Name = "parameter",
                ParameterType = typeof(Device)
            };

            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    RequestServices =
                        serviceProvider
                },
                RouteData = new RouteData()
            };

            var modelMetadata = metadataProvider.GetMetadataForType(parameter.ParameterType);

            var valueProvider =
                new FormValueProvider(BindingSource.Form, testCase.formCollection, CultureInfo.CurrentCulture);

            var modelBinder = modelBinderFactory.CreateBinder(new ModelBinderFactoryContext()
            {
                BindingInfo = parameter.BindingInfo,
                Metadata = modelMetadata,
                CacheToken = parameter
            });

            // Act
            var modelBindingResult = await parameterBinder.BindModelAsync(
                controllerContext,
                modelBinder,
                valueProvider,
                parameter,
                modelMetadata,
                value: null);

            // Assert
            Assert.True(modelBindingResult.IsModelSet);
            modelBindingResult.Model.Should().BeEquivalentTo(testCase.expected);
        }
    }
}