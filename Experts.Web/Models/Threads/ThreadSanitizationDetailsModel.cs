using System.Collections.Generic;
using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class ThreadSanitizationDetailsModel : ThreadDetailsModel
    {
        public ThreadSanitizationDetailsModel()
        {
            SetAvailableStatuses();
        }

        public ThreadSanitizationDetailsModel(Thread thread)
            :base(thread)
        {
            SetAvailableStatuses();
            SelectedStatus = thread.SanitizationStatus;
        }

        private void SetAvailableStatuses()
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
        public ThreadSanitizationStatus SelectedStatus { get; set; }
    }
}