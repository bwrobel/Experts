using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using Experts.Web.Validation;

namespace Experts.Web.Helpers
{
    public static class FormHelper
    {
        private const int TextAreaRows = 4;

        public static IDictionary<string, object> GetHtmlAttributes(ModelMetadata property)
        {
            var result = new Dictionary<string, object>();

            var customAttributes = new object[10];

            if (property.ContainerType != null)
                customAttributes = property.ContainerType.GetProperty(property.PropertyName).GetCustomAttributes(true);

            if (property.IsRequired)
                result.Add("required", true);

            if (property.DataTypeName == DataType.EmailAddress.ToString())
                result.Add("type", "email");

            if (property.DataTypeName == DataType.MultilineText.ToString())
                result.Add("rows", TextAreaRows);

            if (property.ModelType == typeof(decimal?))
            {
                result.Add("class", "decimal");
            }

            if (property.ContainerType != null && customAttributes.Any(a => a.GetType() == typeof(AutoFocusAttribute)))
            {
                result.Add("autofocus", "autofocus");
            }

            return result;
        }
    }
}