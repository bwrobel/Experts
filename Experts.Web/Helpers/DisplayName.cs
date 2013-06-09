using Experts.Core.Entities;

namespace Experts.Web.Helpers
{
    public static class DisplayName
    {
        public static string GlobalName(this User user, bool hideName = true)
        {
            if (user.IsExpert)
                return user.Expert.PublicName;

            if (user.IsModerator)
            {
                if(hideName)
                    return Resources.Thread.HiddenName;
                return user.Moderator.PublicName;
            }

            if (hideName)
                return Resources.Thread.HiddenName;

            return user.Email;
        }
    }
}