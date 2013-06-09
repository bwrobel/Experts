using System;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Core.Utils;
using System.Web.Mvc;

namespace Experts.Web.Utils
{
    public class SystemPostFactory
    {
        public static Post BuildGiveUpPost(Consultant consultant, string reason)
        {
            return new Post
                       {
                           Author = consultant.User,
                           Type = PostType.GiveUp,
                           Content = string.Format(Resources.Thread.GiveUpPostContent, reason)
                       };
        }

        public static Post BuildReservedPost(Consultant consultant, Expert expert)
        {
            return new Post
                       {
                           Author = consultant.User,
                           Type = PostType.Reserved,
                           Content =
                               string.Format(Resources.Thread.ThreadReservedQuestionPost, expert.PublicName)
                       };
        }

        public static Post BuildReleasedPost(Consultant consultant)
        {
            return new Post
                       {
                           Author = consultant.User,
                           Type = PostType.Released,
                           Content = Resources.Thread.ThreadReleasedReservedQuestionPost
                       };
        }

        public static Post BuildAnalyzingPost(Consultant consultant, Expert expert)
        {
            return new Post
                       {
                           Author = consultant.User,
                           Type = PostType.Analyzing,
                           Content = string.Format(Resources.Thread.AnalyzingPostContent, expert.PublicName)
                       };
        }

        public static Post BuildClosedByModeratorPost(Moderator moderator, string reason)
        {
            return new Post
            {
                Author = moderator.User,
                Type = PostType.Answer,
                Content = string.Format(Resources.Thread.ClosedByModeratorPost, moderator.PublicName, reason)
            };
        }

        public static Post BuildChangedCategoryByModeratorPost(Moderator moderator, string reason)
        {
            return new Post
            {
                Author = moderator.User,
                Type = PostType.Answer,
                Content = string.Format(Resources.Thread.ChangedCategoryByModeratorPost, moderator.PublicName, reason)
            };
        }

        public static Post BuildAdditionalServicePost(Consultant consultant, string service)
        {
            return new Post
                             {
                                 Author = consultant.User,
                                 Type = PostType.Info,
                                 Content = string.Format(Resources.Thread.AdditionalServiceAcceptedByUser, service)
                             };
        }

        public static void ChangePostToAnswered(Post post, Expert expert)
        {
            post.Type = PostType.Answered;
            post.Content = string.Format(Resources.Thread.AnsweredPostContent, expert.PublicName);
        }

        public static Post BuildAwaitingExpertResponsePost(Consultant consultant)
        {
            return new Post
                       {
                           Author = consultant.User,
                           Type = PostType.Info,
                           Content = Resources.Thread.AwaitingExpertResponse
                       };
        }
    }
}