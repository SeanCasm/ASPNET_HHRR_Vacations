namespace ASPNET_HHRR_Vacations.Helpers
{
    public static class StringUtilities
    {
        private const string domain = "@enterprise.com";
        /// <summary>
        /// Adds @enterprise.com domain at the end of the email string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>A string with @enterprise.com</returns>
        public static string FormatEmail(this string str)
        {
            if (str.EndsWith(domain))
                return str;

            return (str + domain).ToLower().Trim();
        }
        /// <summary>
        /// Capitalize a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns>A new string lowered and capitalized.</returns>
        public static string Capitalize(this string str)
        {
            char capitalLetter = char.ToUpper(str[0]);
            string newStr = capitalLetter + str.Remove(0, 1).ToLower();
            return newStr;
        }
    }
}
