using Newtonsoft.Json;

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
            return RemoveFirstSlash(Uri.UnescapeDataString(result));

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

        public static bool IsValidHttpMethod(string method)
        {
            return method == HttpMethod.Get.Method ||
                   method == HttpMethod.Post.Method ||
                   method == HttpMethod.Put.Method ||
                   method == HttpMethod.Delete.Method ||
                   method == HttpMethod.Patch.Method ||
                   method == HttpMethod.Head.Method ||
                   method == HttpMethod.Options.Method;
        }

        public static bool IsValidJSON(string json)
        {
            try
            {
                var jsonData = JsonConvert.DeserializeObject(json);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }
    }
}
