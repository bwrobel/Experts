using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Resources;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;

namespace Experts.Web.Helpers
{
    public static class EnumHelper
    {
        public static string Describe(this Enum enumValue, ResourceManager resourceManager)
        {
            var resourceKey = enumValue.GetType().Name + enumValue;
            var description = resourceManager.GetString(resourceKey);

            return string.IsNullOrEmpty(description)
                       ? "<" + resourceKey + ">"
                       : description;

        }

        public static MvcHtmlString DropdownListForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, ResourceManager resourceManager, object htmlAttributes = null)
            where TModel : class
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model;

            var selectListItems = new List<SelectListItem>();
            foreach (Enum item in Enum.GetValues(typeof(TProperty)))
                selectListItems.Add(new SelectListItem
                                   {
                                       Value = item.ToString(),
                                       Text = item.Describe(resourceManager),
                                       Selected = item.ToString() == value.ToString()
                                   });

            var name = ExpressionHelper.GetExpressionText(expression);
            return htmlHelper.DropDownList(name, selectListItems, htmlAttributes);
        }

        public static MvcHtmlString EditorForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, ResourceManager resourceManager, object htmlAttributes = null)
            where TModel : class
        {
            var labelTag = new TagBuilder("div")
                               {
                                   InnerHtml = htmlHelper.LabelFor(expression, new {@class="control-label"}).ToHtmlString()
                               };

            var editorTag = new TagBuilder("div")
                                {
                                    InnerHtml =
                                        htmlHelper.DropdownListForEnum(expression, resourceManager, htmlAttributes).
                                        ToString()
                                };

            return new MvcHtmlString(string.Format("{0} {1}", labelTag, editorTag));
        }
    }
}
