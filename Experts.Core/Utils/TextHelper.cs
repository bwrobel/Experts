namespace Experts.Core.Utils
{
    public static class TextHelper
    {
        public static string TruncateToWholeWord(this string text, int maxLength, bool addDots = false)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";
            if (text.Length <= maxLength) return text;
            for (int i = maxLength; i >= 1; i--)
                if ((char.IsWhiteSpace(text[i]) || char.IsSeparator(text[i]) || char.IsPunctuation(text[i])) && (!char.IsWhiteSpace(text[i - 1]) && !char.IsSeparator(text[i - 1]) && !char.IsPunctuation(text[i - 1])))
                {
                    if (addDots && text.Length > maxLength) { return text.Substring(0, i) + "..."; }
                    return text.Substring(0, i);   
                }

            if (addDots && text.Length > maxLength) { return text.Substring(0, maxLength) + "..."; }

            return text.Substring(0, maxLength); //there is no special characters , we just truncate
        }
    }
}