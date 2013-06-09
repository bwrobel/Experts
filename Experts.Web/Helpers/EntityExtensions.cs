using System;
using System.Collections.Generic;
using System.Linq;
using Experts.Core.Entities;

namespace Experts.Web.Helpers
{
    public static class EntityExtensions
    {
        public static bool IsFirstInThread(this Post post)
        {
            return post.Thread.Posts.OrderBy(p => p.Id).First().Id == post.Id;
        }

        public static bool IsLastInPost(this Attachment attachment)
        {
            return attachment.Post.Attachments.OrderBy(a => a.Id).Last().Id == attachment.Id;
        }

        public static bool IsLastInThread(this Post post)
        {
            return post.Thread.Posts.OrderBy(p => p.Id).Last().Id == post.Id;
        }

        public static bool IsEditable(this Post post)
        {
            if (post.Thread.State == ThreadState.Closed)
                return false;

            var editableTypes = new List<PostType>
                                    {PostType.Question, PostType.Answer, PostType.DetailsRequest, PostType.Details, PostType.ModeratorAnswer, PostType.ExpertPM};

            var hasPermissions = AuthenticationHelper.IsModerator ||
                                 (!post.IsReadOnly && post.Author == AuthenticationHelper.CurrentUser);

            return hasPermissions && editableTypes.Contains(post.Type);
        }

        public static bool IsDeletable(this Post post)
        {
            var deletableTypes = new List<PostType>
                                     {PostType.Answer, PostType.Attachment, PostType.DetailsRequest, PostType.Details};
            return !post.IsReadOnly && post.Author == AuthenticationHelper.CurrentUser &&
                   deletableTypes.Contains(post.Type);
        }

        public static bool IsVisibleOnlyForAuthor(this Post post)
        {
            var visibleOnlyForAuthor = new List<PostType> {PostType.Analyzing, PostType.Answered};
            return visibleOnlyForAuthor.Contains(post.Type);
        }

        public static bool IsVisibleOnlyForExpert(this Post post)
        {
            return post.Type == PostType.ExpertPM;
        }

        public static IDictionary<string, string> GetCategoryAttributesForDisplay(this Thread thread)
        {
            var result = new Dictionary<string, string>();

            foreach (var attribute in thread.CategoryAttributes)
            {
                switch (attribute.Attribute.Type)
                {
                    case CategoryAttributeType.SingleLineText:   
                    case CategoryAttributeType.MultiLineText:
                        result.Add(attribute.Attribute.Name, attribute.Value);
                        break;
                    case CategoryAttributeType.SingleSelect:
                        result.Add(attribute.Attribute.Name, attribute.SelectedOptions.Single().Value);
                        break;
                    case CategoryAttributeType.MultiSelect:
                        result.Add(attribute.Attribute.Name, string.Join(", ", attribute.SelectedOptions.Select(s => s.Value)));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return result;
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach(var item in items)
                collection.Add(item);
        }
    }
}