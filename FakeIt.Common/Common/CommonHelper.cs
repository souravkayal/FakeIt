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

            return input.Substring(index + keyword.Length);
        }
    }
}
