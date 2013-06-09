using System.Collections.Generic;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Threads
{
    public class SanitizationQuestionListModel
    {
        public SanitizationQuestionListModel()
        {
            var statuses = new List<SelectListItem>
                               {
                                   new SelectListItem { Text = Resources.Thread.ThreadSanitizationStatusNotSanitized, Value = ThreadSanitizationStatus.NotSanitized.ToString() },
                                   new SelectListItem{Text = Resources.Thread.ThreadSanitizationStatusSanitized, Value = ThreadSanitizationStatus.Sanitized.ToString() },
                                   new SelectListItem{ Text = Resources.Thread.ThreadSanitizationStatusNotForPublic, Value = ThreadSanitizationStatus.NotForPublic.ToString() }
                               };

            AvailableStatuses = statuses;
        }

        public IEnumerable<SelectListItem> AvailableStatuses { get; private set; }
        public SortableGridModel<Thread> GridModel { get; set; }
        public ThreadSanitizationStatus SelectedStatus { get; set; }
    }
}