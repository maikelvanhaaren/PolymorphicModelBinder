using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries.Discriminator
{
    public class DiscriminatorPolymorphicBindable : IPolymorphicBindable
    {
        private readonly string _discriminatorFieldName;
        private readonly Func<string, bool> _valueMatch;

        public DiscriminatorPolymorphicBindable(Type bindToType, string discriminatorFieldName) 
            : this(bindToType, discriminatorFieldName, value => value == bindToType.Name)
        {
        }

        public DiscriminatorPolymorphicBindable(Type bindToType, string discriminatorFieldName, Func<string, bool> valueMatch)
        {
            BindToType = bindToType;
            _discriminatorFieldName = discriminatorFieldName;
            _valueMatch = valueMatch;
        }
    
        public Type BindToType { get; set; }
    
        public bool IsBindable(ModelBindingContext bindingContext)
        {
            var fieldName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, _discriminatorFieldName);
            var value = bindingContext.ValueProvider.GetValue(fieldName);

            if (value == ValueProviderResult.None)
                throw new ArgumentNullException(_discriminatorFieldName);

            return _valueMatch(value.FirstValue);
        }
    }
}