namespace ToKO.TestUtils
{
    public static class StringExtension
    {
        public static string Simplify(this string s)
        {
            return s.Replace("\t", "").Replace("\n", "").Replace("\r", "");
        }
    }
}