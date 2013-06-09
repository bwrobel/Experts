using System;
using System.IO;
using System.Net;
using System.Web;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using Quartz;
using System.Linq;

namespace Experts.Web.Jobs
{
    public class TemporaryAttachmentFolderJob : JobWithErrorHandling
    {
        public override void DoJob(IJobExecutionContext context)
        {
            var attachmentFolder = new DirectoryInfo(Path.Combine(HttpRuntime.AppDomainAppPath, "Attachments"));
            var temporaryAttachmentFolders = attachmentFolder.GetDirectories()
                .Where(f => f.Name.StartsWith("temp") && f.CreationTime.AddDays(1) < DateTime.Now );

            foreach (var folder in temporaryAttachmentFolders)
                folder.Delete(true);
        }
    }
}