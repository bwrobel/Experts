using System.Collections.Generic;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Helpers;
using Experts.Web.Models.Forms;
using System.Linq;

namespace Experts.Web.Models.Threads
{
    public class ThreadFormModel
    {
        public ThreadFormModel(IEnumerable<Category> availableCategories)
        {
            AvailableCategories = availableCategories;
            AvailableCategoriesItems = CategoryHelper.GetCategoryListItems(availableCategories, Resources.Thread.SelectCategory);

            ThreadForm = new ThreadForm();
            if(ThreadForm.TemporaryAttachmentFolder == null)
                ThreadForm.GenerateTemporaryAttachmentFolder();
        }

        public ThreadFormModel() { }

        public IEnumerable<Category> AvailableCategories { get; set; }

        public IEnumerable<SelectListItem> AvailableCategoriesItems { get; set; }

        public Category SelectedCategory
        {
            get
            {
                if (ThreadForm != null && ThreadForm.CategoryId.HasValue)
                    return AvailableCategories.Single(c => c.Id == ThreadForm.CategoryId);

                return null;
            }
        }

        public IEnumerable<Category> MainCategories
        {
            get
            {
                if (SelectedCategory == null)
                    return AvailableCategories.Take(6);

                var result = new List<Category> {SelectedCategory};
                result.AddRange(AvailableCategories.Where(c => c != SelectedCategory).Take(5));
                return result;
            }
        }

        public IEnumerable<Category> MoreCategories
        {
            get
            {
                if (SelectedCategory == null)
                    return AvailableCategories.Skip(6);

                return AvailableCategories.Where(c => c != SelectedCategory).Skip(12); 
            }
        }

        public ThreadForm ThreadForm { get; set; }
    }
}