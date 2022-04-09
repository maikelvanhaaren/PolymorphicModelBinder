using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder.Builders;

public interface IBindPolymorphicBuilder<in TAbstract>
{
    /// <summary>
    /// Match implementation type based on the parsed type in a value
    /// </summary>
    /// <typeparam name="TImplementation"></typeparam>
    /// <returns></returns>
    IBindPolymorphicBuilder<TAbstract> AddFromTypeInValue<TImplementation>() where TImplementation : TAbstract, new();
    
    /// <summary>
    /// Match implementation based on custom logic
    /// </summary>
    /// <param name="isMatchFunc"></param>
    /// <typeparam name="TImplementation"></typeparam>
    /// <returns></returns>
    IBindPolymorphicBuilder<TAbstract> AddFromCustom<TImplementation>(Func<ModelBindingContext, bool> isMatchFunc) where TImplementation : TAbstract, new();

    /// <summary>
    /// Add a new implementation entry
    /// </summary>
    /// <param name="implementation"></param>
    /// <returns></returns>
    IBindPolymorphicBuilder<TAbstract> Add(IPolymorphicImplementation implementation);

}