using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PolymorphicModelBinder.Entries.Custom
{
    public class CustomPolymorphicBindable : IPolymorphicBindable
    {
        private readonly Func<ModelBindingContext, bool> _isMatchFunc;

        public CustomPolymorphicBindable(Type type, Func<ModelBindingContext, bool> isMatchFunc)
        {
            BindToType = type;
            _isMatchFunc = isMatchFunc;
        }

        public Type BindToType { get; }

        public bool IsBindable(ModelBindingContext bindingContext) => _isMatchFunc(bindingContext);
    }
}