using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder.Builders
{
    public interface IBindPolymorphicBuilder<in TAbstract>
    {
        /// <summary>
        /// Match implementation type based on the parsed type in value. Use @Html.PolymorphicTypeInValueFor(...) for defining the value.
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam> 
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromTypeInValue<TImplementation>() where TImplementation : TAbstract, new();

        /// <summary>
        /// Match implementation type based on discriminator field.
        /// Default field name: 'Discriminator'
        /// Default value match: 'typeof(TImplementation).Name == value'
        /// </summary>
        /// <param name="fieldName">Discriminator field name (default: 'Discriminator')</param>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromDiscriminator<TImplementation>()
            where TImplementation : TAbstract, new();

        /// <summary>
        /// Match implementation type based on discriminator field.
        /// Default field name: 'Discriminator'
        /// Default value match: 'typeof(TImplementation).Name == value'
        /// </summary>
        /// <param name="fieldName">Discriminator field name (default: 'Discriminator')</param>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromDiscriminator<TImplementation>(
            string fieldName) 
            where TImplementation : TAbstract, new();
        
        /// <summary>
        /// Match implementation type based on discriminator field.
        /// Default field name: 'Discriminator'
        /// Default value match: 'typeof(TImplementation).Name == value'
        /// </summary>
        /// <param name="discriminatorValueMatch">Discriminator value match (default: 'typeof(TImplementation).Name == value')</param>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromDiscriminator<TImplementation>(
            Func<string, bool> discriminatorValueMatch) 
            where TImplementation : TAbstract, new();
        
        /// <summary>
        /// Match implementation type based on discriminator field.
        /// Default field name: 'Discriminator'
        /// Default value match: 'typeof(TImplementation).Name == value'
        /// </summary>
        /// <param name="fieldName">Discriminator field name (default: 'Discriminator')</param>
        /// <param name="discriminatorValueMatch">Discriminator value match (default: 'typeof(TImplementation).Name == value')</param>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromDiscriminator<TImplementation>(
            string fieldName,
            Func<string, bool> discriminatorValueMatch) 
            where TImplementation : TAbstract, new();

        /// <summary>
        /// Match implementation based on custom logic
        /// Default field name: 'Discriminator'
        /// Default value match: 'typeof(TImplementation).Name == value'
        /// </summary>
        /// <param name="isMatchFunc"></param>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> AddFromCustom<TImplementation>(Func<ModelBindingContext, bool> isMatchFunc) where TImplementation : TAbstract, new();

        /// <summary>
        /// Add a new implementation entry
        /// </summary>
        /// <param name="bindable"></param>
        /// <returns></returns>
        IBindPolymorphicBuilder<TAbstract> Add(IPolymorphicBindable bindable);

    }
}