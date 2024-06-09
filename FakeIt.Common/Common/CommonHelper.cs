namespace FakeIt.Common.Common
{
    public static class CommonHelper
    {
        public static string GetPartAfterRead(string input)
        {
            string keyword = "/read";
            int index = input.IndexOf(keyword);

            if (index == -1)
            {
                return string.Empty;
            }

            var result = input.Substring(index + keyword.Length);

            return Uri.UnescapeDataString(result);

            //return RemoveFirstSlash(Uri.UnescapeDataString(result));
        }

        public static string RemoveFirstSlash(string url)
        {
            int index = url.IndexOf('/');
            if (index != -1)
            {
                return url.Substring(0, index) + url.Substring(index + 1);
            }
            return url; // Return the original url if there's no slash
        }
    }
}
