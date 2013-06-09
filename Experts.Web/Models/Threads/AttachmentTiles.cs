using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Experts.Web.Models.Forms;

namespace Experts.Web.Models.Threads
{
    public class AttachmentTiles
    {
        public List<string> FileNames { get; set; }
        public string TemporaryAttachmentFolder { get; set; }
        public bool IsTiny { get; set; }
    }
}