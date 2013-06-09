using System;
using System.Collections.Generic;
using Experts.Core.Entities;
using System.Web;

namespace Experts.Web.Helpers
{
    public static class UploadHelper
    {
        private const int AvatarSizeLimit = 2097152; //2MB
        private const int AttachmentSizeLimit = 10380902; //10MB

        public enum FileType
        {
            Avatar = 1,
            PostAttachment = 2
        }
        
        public static Dictionary<string, AttachmentType> WhiteList(bool allowOnlyImages = false)
        {
            var whiteList = new Dictionary<string, AttachmentType>();

            whiteList.Add("image/png", AttachmentType.Image);
            whiteList.Add(".png", AttachmentType.Image);

            whiteList.Add("image/bmp", AttachmentType.Image);
            whiteList.Add(".bmp", AttachmentType.Image);

            whiteList.Add("image/jpeg", AttachmentType.Image);
            whiteList.Add(".jpg", AttachmentType.Image);

            if (allowOnlyImages)
                return whiteList;

            whiteList.Add("text/plain", AttachmentType.Document);
            whiteList.Add(".txt", AttachmentType.Document);

            whiteList.Add("application/pdf", AttachmentType.Document);
            whiteList.Add(".pdf", AttachmentType.Document);

            whiteList.Add("application/msword", AttachmentType.Document);
            whiteList.Add(".doc", AttachmentType.Document);


            whiteList.Add("audio/mp3", AttachmentType.Audio);
            whiteList.Add(".mp3", AttachmentType.Audio);

            whiteList.Add("audio/ogg", AttachmentType.Audio);
            whiteList.Add(".ogg", AttachmentType.Audio);

            whiteList.Add("audio/mpeg", AttachmentType.Audio);
            whiteList.Add(".mpeg", AttachmentType.Audio);

            whiteList.Add("audio/wav", AttachmentType.Audio);
            whiteList.Add(".wav", AttachmentType.Audio);

            whiteList.Add("audio/mp4", AttachmentType.Audio);
            whiteList.Add(".mp4", AttachmentType.Audio);

            whiteList.Add("audio/mpeg4", AttachmentType.Audio);
            whiteList.Add(".mpeg4", AttachmentType.Audio);


            whiteList.Add("video/avi", AttachmentType.Video);
            whiteList.Add(".avi", AttachmentType.Video);

            return whiteList;
        }

        public static AttachmentType SpecifyAttachmentType(string contentTypeOrFileExtension)
        {
            var whiteList = WhiteList();
            return whiteList[contentTypeOrFileExtension];
        }

        public static bool ValidateUpload(int size, string contentTypeOrFileExtension, FileType fileType)
        {
            if(fileType == FileType.Avatar)
            {
                if(size < AvatarSizeLimit && size > 0 && WhiteList(true).ContainsKey(contentTypeOrFileExtension))
                    return true;
            }
            if(fileType == FileType.PostAttachment)
            {
                if (size < AttachmentSizeLimit && size > 0 && WhiteList().ContainsKey(contentTypeOrFileExtension))
                    return true;
            }
            return false;
        }

        public static string RoundBytes(int attachmentSize, int decimals)
        {
            var mbSize = attachmentSize/1048576.0;
            var kbSize = attachmentSize/1024.0;

            if(mbSize < 0.1){
                return "(" + Math.Round(kbSize, decimals, MidpointRounding.AwayFromZero).ToString() + " KB)";
            }

            return "(" + Math.Round(mbSize, decimals, MidpointRounding.AwayFromZero).ToString() + " MB)";
        }
    }
}