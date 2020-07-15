namespace SOArchitecture
{
    public static class SOArchitectureHelpers
    {
        public static string ToTitle(this string value) =>
            string.Concat(value[0].ToString().ToUpper(), value.Substring(1, value.Length - 1));
    }
}
