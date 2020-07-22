namespace SOArchitecture
{
    public static class SOArchitectureHelpers
    {
        /// <summary>
        /// Static method that extens the String class
        /// It can take a string and change the first letter of each word to upper form
        /// </summary>
        /// <param name="value">Parameter that indicates the string that will be changed</param>
        /// <returns>The new string in Title form</returns>
        public static string ToTitle(this string value) =>
            string.Concat(value[0].ToString().ToUpper(), value.Substring(1, value.Length - 1));
    }
}
