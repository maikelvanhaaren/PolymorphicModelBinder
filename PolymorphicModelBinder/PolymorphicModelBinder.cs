using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PolymorphicModelBinder.Entries;

namespace PolymorphicModelBinder;

internal class PolymorphicModelBinder : IModelBinder
{
    private readonly List<(IPolymorphicImplementation entryType, ModelMetadata modelMetadata, IModelBinder modelBinder)> _binders;

    public PolymorphicModelBinder(List<(IPolymorphicImplementation, ModelMetadata, IModelBinder)> binders)
    {
        _binders = binders;
    }

    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var entryFound = _binders
            .Any(binder => binder.entryType.IsBindable(bindingContext));

        if (!entryFound)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        } 

        var (_, modelMetadata, modelBinder) = _binders
            .First(binder => binder.entryType.IsBindable(bindingContext));
        
        var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
            bindingContext.ActionContext,
            bindingContext.ValueProvider,
            modelMetadata,
            bindingInfo: null,
            bindingContext.ModelName);

        await modelBinder.BindModelAsync(newBindingContext).ConfigureAwait(false);
        bindingContext.Result = newBindingContext.Result;

        if (newBindingContext.Result.IsModelSet)
        {
            bindingContext.ValidationState[newBindingContext.Result.Model!] = new ValidationStateEntry
            {
                Metadata = modelMetadata,
            };
        }
    }
}