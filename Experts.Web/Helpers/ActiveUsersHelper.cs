using System;
using System.Collections.Generic;
using System.Linq;
using Experts.Core.Entities;

namespace Experts.Web.Helpers
{
    public static class ActiveUsersHelper
    {
        private const int AllowedInactivity = 15; // minutes

        private static readonly Dictionary<int, DateTime> UserLastActivities = new Dictionary<int, DateTime>();
        private static readonly Dictionary<int, ExpertInfo> ActiveExperts = new Dictionary<int, ExpertInfo>();
        private static readonly Dictionary<int, DateTime> ActiveModerators = new Dictionary<int, DateTime>();
        private static readonly Dictionary<int, DateTime> ActiveChatModerators = new Dictionary<int, DateTime>();

        public static int ActiveUsersCount { get { return UserLastActivities.Count; } }

        public static int ActiveExpertsCount { get { return ActiveExperts.Count; } }

        public static void SetCurrentUserActive()
        {
            var user = AuthenticationHelper.CurrentUser;
            var now = DateTime.Now;
            
            if (user == null)
                return;

            if (user.IsExpert && (user.Expert.IsInner || !user.Expert.IsVerified))  // TODO: tymczasowy fix żeby nie pokazywać niezweryfikowanych
                return;

            UserLastActivities[user.Id] = now;

            if (user.IsExpert)
            {
                ActiveExperts[user.Id] = new ExpertInfo
                    {
                        IsPublic = !user.Expert.IsInner && user.Expert.IsVerified,
                        CategoryIds = user.Expert.Categories.Select(c => c.Id)
                    };
            }

            if (user.IsModerator)
                ActiveModerators[user.Id] = now;
                
        }

        public static void SetCurrentModeratorChatActive()
        {
            var user = AuthenticationHelper.CurrentUser;
            var now = DateTime.Now;
            ActiveChatModerators[user.Id] = now;
        }

        public static ChatModeratorStatus GetChatModeratorStatus()
        {
            return ActiveChatModerators.Any()
                ? ChatModeratorStatus.Active
                : ActiveModerators.Any()
                        ? ChatModeratorStatus.Away
                        : ChatModeratorStatus.Offline;
        }

        public static void RemoveInactiveUsers()
        {
            var requiredActivity = DateTime.Now.AddMinutes(-AllowedInactivity);

            var inactiveUsers = (from lastActivity in UserLastActivities where lastActivity.Value < requiredActivity select lastActivity.Key).ToList();

            foreach (var inactiveUser in inactiveUsers)
            {
                UserLastActivities.Remove(inactiveUser);
                ActiveExperts.Remove(inactiveUser);
                ActiveModerators.Remove(inactiveUser);
            }

            var inactiveChatModerators = (from lastActivity in ActiveChatModerators where lastActivity.Value < requiredActivity select lastActivity.Key).ToList();

            foreach (var inactiveChatModerator in inactiveChatModerators)
            {
                ActiveChatModerators.Remove(inactiveChatModerator);
            }

        }

        public static int GetActiveAndPublicExpertsCountForCategory(int categoryId)
        {
            return ActiveExperts.Count(ae => ae.Value.IsPublic && ae.Value.CategoryIds.Contains(categoryId));
        }

        public static IEnumerable<int> GetActivePublicExpertsIds(int? categoryId)
        {
            if (categoryId.HasValue)
                return ActiveExperts.Where(ae => ae.Value.IsPublic && ae.Value.CategoryIds.Contains(categoryId.Value)).Select(ae => ae.Key);
            
            return ActiveExperts.Select(ae => ae.Key);
        }

        public static bool IsActive(int userId)
        {
            return UserLastActivities.ContainsKey(userId);
        }

        private class ExpertInfo
        {
            public bool IsPublic { get; set; }
            public IEnumerable<int> CategoryIds { get; set; }
        }
    }
}