using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries.TypeInValue
{
    internal class TypeInValuePolymorphicBindable : IPolymorphicBindable
    {
        public const string FieldName = "TypeInValuePolymorphic";

        public TypeInValuePolymorphicBindable(Type type)
        {
            BindToType = type;
        }

        public Type BindToType { get; }

        public bool IsBindable(ModelBindingContext bindingContext)
        {
            var fieldName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, FieldName);
            var value = bindingContext.ValueProvider.GetValue(fieldName);

            if (value == ValueProviderResult.None)
                return false;

            return Type.GetType(value.FirstValue) == BindToType;
        }
    }
}