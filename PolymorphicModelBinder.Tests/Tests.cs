using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using PolymorphicModelBinder.Tests.Models;

namespace PolymorphicModelBinder.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        var services = new ServiceCollection();

        services.AddPolymorphicModelBinder(options =>
        {
            options.Add<Device>(polymorphicBuilder =>
            {
                polymorphicBuilder.AddFromTypeInValue<Laptop>();
                polymorphicBuilder.AddFromTypeInValue<SmartPhone>();
            });
        });
        
        Assert.Pass();
    }
}