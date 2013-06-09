using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Web.Models.Shared;
using Experts.Core.Repositories;

namespace Experts.Web.Helpers
{
    public class CategoryAttributeHelper
    {
        public CategoryAttributeHelper()
        {
            _repository = RepositoryHelper.Repository;
        }

        private readonly RepositoryFactory _repository;

        public int GetCategoryCount()
        {
            return _repository.Category.All().Count();
        }

        public IEnumerable<CategoryAttributeValue> GetCategoryAttributeValues(IEnumerable<AttributeValueModel> attributeValues, bool isForExpert = false)
        {
            if (attributeValues != null)
            {
                foreach (var attributeValue in attributeValues)
                {
                    var attribute = _repository.Category.GetCategoryAttribute(attributeValue.AttributeId);

                    if (isForExpert)
                    {
                        var value = GetCategoryAttributeValuesForMultiselect(attribute, attributeValue);
                        if (value != null)
                            yield return value;
                    }
                    else
                        switch (attribute.Type)
                        {
                            case CategoryAttributeType.SingleLineText:
                            case CategoryAttributeType.MultiLineText:
                                if (!string.IsNullOrEmpty(attributeValue.Value))
                                    yield return
                                        new CategoryAttributeValue { Attribute = attribute, Value = attributeValue.Value };
                                break;
                            case CategoryAttributeType.SingleSelect:
                                var selectedOption =
                                    attribute.Options.SingleOrDefault(o => o.Id == int.Parse(attributeValue.Value));
                                if (selectedOption != null)
                                {
                                    var singleSelectSelectedOptions = new Collection<CategoryAttributeOption> { selectedOption };
                                    yield return
                                        new CategoryAttributeValue { Attribute = attribute, SelectedOptions = singleSelectSelectedOptions };
                                }
                                break;
                            case CategoryAttributeType.MultiSelect:
                                var value = GetCategoryAttributeValuesForMultiselect(attribute, attributeValue);
                                if (value != null)
                                    yield return value;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                }
            }
        }

        private CategoryAttributeValue GetCategoryAttributeValuesForMultiselect(CategoryAttribute attribute, AttributeValueModel attributeValue)
        {
            var selectedOptions = new Collection<CategoryAttributeOption>();
            for (var i = 0; i < attribute.Options.Count; i++)
                if (bool.Parse(attributeValue.Values[i]))
                    selectedOptions.Add(attribute.Options.ElementAt(i));

            return selectedOptions.Any() ? new CategoryAttributeValue { Attribute = attribute, SelectedOptions = selectedOptions } : null;
        }

        public static AttributeValueModel[] GetCategoryAttributeValueModel(IEnumerable<CategoryAttributeValue> attributeValues, bool forExpert = false)
        {
            List<AttributeValueModel> results = new List<AttributeValueModel>();
            foreach(var value in attributeValues)
            {
                
                var selectedValues = new List<string>();

                if (value.Attribute.Type == CategoryAttributeType.MultiSelect || forExpert)
                {
                    foreach (var option in value.Attribute.Options)
                    {
                        var isSelected = value.SelectedOptions.Contains(option);
                        selectedValues.Add(isSelected.ToString());
                    }
                }

                var model = new AttributeValueModel 
                    {
                        AttributeId = value.Attribute.Id,
                        Value = value.Attribute.Type == CategoryAttributeType.SingleSelect && !forExpert ? value.SelectedOptions.Single().Id.ToString() : value.Value, 
                        Values = selectedValues.ToArray() 
                    };
                
                results.Add(model);
            }
            return results.ToArray();
        }
    }
}