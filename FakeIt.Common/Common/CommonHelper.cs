using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace FakeIt.Common.Common
{
    public static class CommonHelper
    {
        public static string GetPartAfterRead(string input)
        {
            int firstSlashIndex = input.IndexOf('/');

            int secondSlashIndex = input.IndexOf('/', firstSlashIndex + 1);

            if (secondSlashIndex != -1)
            {
                return RemoveFirstSlash(input.Substring(secondSlashIndex));
            }

            return string.Empty;
        }

        public static string GetProjectNameFromURI(string uri) 
        {
            int firstSlashIndex = uri.IndexOf('/');

            int secondSlashIndex = uri.IndexOf('/', firstSlashIndex + 1);

            if (firstSlashIndex != -1 && secondSlashIndex != -1)
            {
                return uri.Substring(firstSlashIndex + 1, secondSlashIndex - firstSlashIndex - 1);
            }
            return string.Empty;    
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
            if (json == null)
                return true;

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

        public static bool IsValidHttpStatusCode(int statusCode)
        {
            return Enum.IsDefined(typeof(HttpStatusCode), statusCode);
        }

        public static bool IsValidURI(string uri)
        {
            string pattern = @"^[a-zA-Z0-9/]+$";
            
            return Regex.IsMatch(uri, pattern) ? true : false;
        }

        public static string RemoveFirstSlashFromURI(string input)
        {
            // Check if the string starts with "/"
            if (!string.IsNullOrEmpty(input) && input.StartsWith("/"))
            {
                // Remove the first character ("/")
                return input.Substring(1);
            }

            // If the string does not start with "/", return it unchanged
            return input;
        }

    }
}
