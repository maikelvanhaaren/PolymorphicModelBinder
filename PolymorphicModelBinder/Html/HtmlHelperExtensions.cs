using System;
using System.Collections;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using PolymorphicModelBinder.Entries.TypeInValue;

namespace PolymorphicModelBinder.Html
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Renders the editor template based on the runtime type of the model.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHtmlContent PolymorphicEditorFor<TModel, TProperty>(this IHtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            if (typeof(IEnumerable).IsAssignableFrom(typeof(TProperty)))
                throw new InvalidOperationException($"There is currently no support for using an {nameof(IEnumerable)}. Iterate through the list in the view and call the ${nameof(PolymorphicEditorFor)} method for each item.");
            
            if (htmlHelper.ViewData.Model == null)
                return new HtmlString(string.Empty);

            var templateName = expression.Compile()(htmlHelper.ViewData.Model)?.GetType().Name;

            return htmlHelper.EditorFor(
                expression,
                templateName);
        }


        /// <summary>
        /// Renders hidden field with model type.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IHtmlContent PolymorphicTypeInValue(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper.ViewData.Model == null)
                throw new ArgumentNullException(nameof(htmlHelper.ViewData.Model));

            return htmlHelper
                .Hidden(TypeInValuePolymorphicBindable.FieldName,
                    htmlHelper.ViewData.Model.GetType().AssemblyQualifiedName);
        }

        /// <summary>
        /// Renders hidden field with model type.
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
                .Hidden($"{mvcHtmlString}.{TypeInValuePolymorphicBindable.FieldName}",
                    model.GetType().AssemblyQualifiedName);
        }
    }
}