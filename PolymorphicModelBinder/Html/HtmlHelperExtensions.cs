using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PolymorphicModelBinder.Entries.TypeInValue;

namespace PolymorphicModelBinder.Html;

public static class HtmlHelperExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IHtmlContent PolymorphicTypeInValue(this IHtmlHelper htmlHelper)
    {
        if (htmlHelper.ViewData.Model == null)
            throw new ArgumentNullException(nameof(htmlHelper.ViewData.Model));
        
        return htmlHelper
            .Hidden(TypeInValuePolymorphicImplementation.FieldName, htmlHelper.ViewData.Model.GetType().AssemblyQualifiedName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProp"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IHtmlContent PolymorphicTypeInValueFor<T, TProp>(
        this IHtmlHelper<T> htmlHelper,
        Expression<Func<T, TProp>> expression)
    {
        var mvcHtmlString = htmlHelper.NameFor(expression);
        var model = expression.Compile()(htmlHelper.ViewData.Model);
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        return htmlHelper
            .Hidden($"{mvcHtmlString}.{TypeInValuePolymorphicImplementation.FieldName}", model.GetType().AssemblyQualifiedName);
    }
}