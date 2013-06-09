using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Helpers
{
    public static class ImageHelper
    {
        private const string UserAvatarAlt = "user_avatar";

        private static Image ResizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH > nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            //fix size rounding 
            if (destHeight < destWidth)
                destHeight = size.Height;
            else if (destWidth < destHeight)
                destWidth = size.Width;
            else
            {
                destHeight = size.Height;
                destWidth = size.Width;
            }

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private static Image CropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public static void SaveCroppedAndResizedCentered(string inputFile, int width, int height, string outputFile)
        {
            var image = Image.FromFile(inputFile);

            var resizedImage = ResizeImage(image, new Size(width, height));

            //determine smaller dimension and crop by bigger
            var cropArea = new Rectangle();
            if (resizedImage.Height > resizedImage.Width)
            {
                cropArea.Y = (resizedImage.Height - height)/2;
                cropArea.Height = height;
                cropArea.X = 0;
                cropArea.Width = width;
            }
            else if (resizedImage.Width > resizedImage.Height)
            {
                cropArea.X = (resizedImage.Width - width) / 2;
                cropArea.Width = width;
                cropArea.Y = 0;
                cropArea.Height = height;
            }
            else
            {
                cropArea.X = 0;
                cropArea.Width = width;
                cropArea.Y = 0;
                cropArea.Height = height;
            }

            var croppedImage = CropImage(resizedImage, cropArea);

            croppedImage.Save(outputFile, ImageFormat.Jpeg);
        }

        public static MvcHtmlString ProfileAvatar(this HtmlHelper htmlHelper, User user)
        {

            if (user.IsExpert && user.Expert.HasPicture)
                return htmlHelper.ProfilePicture80x80(user.Expert.Id);

            if (user.IsConsultant)
                return htmlHelper.ConsulantPicture80x80();

            return htmlHelper.DefaultPicture80x80();
        }

        public static MvcHtmlString ProfileBigAvatar(this HtmlHelper htmlHelper, Expert expert)
        {
            if (!expert.HasPicture)
                return htmlHelper.DefaultPicture300x300();

            return htmlHelper.ProfilePicture300x300(expert.Id);
        }

        public static string UrlToConsultant()
        {
            return "/ProfileImages/consultant.jpg";
        }

        public static string UrlToProfilePicture(int expertId)
        {
            return "/ProfileImages/" + expertId + ".jpg";
        }

        public static string UrlToProfilePicture80x80(int expertId)
        {
            return "/ProfileImages/" + expertId + "_80x80.jpg";
        }

        public static string UrlToProfilePicture300x300(int expertId)
        {
            return "/ProfileImages/" + expertId + "_300x300.jpg";
        }

        public static string UrlToUnknownExpertPicture80x80()
        {
            return "/ProfileImages/Default/unknownAvatar.jpg";
        }        
        
        public static string UrlToDefaultPicture300x300()
        {
            return "/ProfileImages/Default/defaultBigPicture.jpg";
        }

        public static string UrlToDefaultPicture80x80()
        {
            return "/ProfileImages/Default/defaultAvatar.jpg";
        }    

        public static string UrlToCategoryIconBig(int categoryId)
        {
            return Links.Content.images.categories.Url(categoryId + "big.png");
        }        
        
        public static string UrlToCategoryIconSmall(int categoryId)
        {
            return Links.Content.images.categories.Url(categoryId + "small.png");
        }

        public static string UrlToCategoryIconTiny(int categoryId)
        {
            return Links.Content.images.categories.Url(categoryId + "tiny.png");
        }

        public static string UrlToFeedbackMarkIcon(FeedbackMark mark)
        {
            return Links.Content.images.thumbs.Url(mark.ToString().ToLower() + "-thumbs.png");
        }

        public static string UrlToFeedbackMarkIconBig(FeedbackMark mark)
        {
            return Links.Content.images.thumbs.Url(mark.ToString().ToLower() + "-thumbs-big.png");
        }

        public static MvcHtmlString ConsulantPicture80x80(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToConsultant(), 80, 80, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString ProfilePicture80x80(this HtmlHelper htmlHelper, int userId, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToProfilePicture80x80(userId), 80, 80, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString ProfilePicture300x300(this HtmlHelper htmlHelper, int userId, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToProfilePicture300x300(userId), 300, 300, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString DefaultPicture80x80(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToDefaultPicture80x80(), 80, 80, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString UnknownExpertPicture80x80(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToUnknownExpertPicture80x80(), 80, 80, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString DefaultPicture300x300(this HtmlHelper htmlHelper, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToDefaultPicture300x300(), 300, 300, UserAvatarAlt, htmlAttributes);
        }

        public static MvcHtmlString CategoryIconBig(this HtmlHelper htmlHelper, Category category, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToCategoryIconBig(category.Id), 36, 36, category.Name, htmlAttributes);
        }

        public static MvcHtmlString CategoryIconSmall(this HtmlHelper htmlHelper, Category category, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToCategoryIconSmall(category.Id), 40, 40, category.Name, htmlAttributes);
        }

        public static MvcHtmlString CategoryIconTiny(this HtmlHelper htmlHelper, Category category, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToCategoryIconTiny(category.Id), 18, 18, category.Name, htmlAttributes);
        }

        public static MvcHtmlString FeedbackMarkIcon(this HtmlHelper htmlHelper, FeedbackMark mark, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToFeedbackMarkIcon(mark), 18, 18, mark.ToString(), htmlAttributes);
        }

        public static MvcHtmlString FeedbackMarkIconBig(this HtmlHelper htmlHelper, FeedbackMark mark, object htmlAttributes = null)
        {
            return htmlHelper.Image(UrlToFeedbackMarkIconBig(mark), 36, 36, mark.ToString(), htmlAttributes);
        }
    }
}