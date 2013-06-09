using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Helpers;
using Experts.Web.Models.Forms;

namespace Experts.Web.Models.Threads
{
    public class PostFormModel
    {
        public PostFormModel(IEnumerable<PostType> availableTypes,int threadId)
        {
            PostForm = new PostForm {ThreadId = threadId};

            var types = availableTypes.Select(c => new SelectListItem { Text = c.Describe(Resources.Thread.ResourceManager), Value = c.ToString() }).ToList();

            if (availableTypes.Count() > 1)
                types.Insert(0, new SelectListItem { Selected = true, Text = Resources.Thread.PostSelectType, Value = string.Empty });
            else
                PostForm.Type = (PostType)Enum.Parse(typeof(PostType), types[0].Value);

            AvailableTypes = types;
        }

        public PostFormModel() { }

        public IEnumerable<SelectListItem> AvailableTypes { get; set; }

        public PostForm PostForm { get; set; }
    }
}