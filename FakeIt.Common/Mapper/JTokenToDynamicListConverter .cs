using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FakeIt.Common.Mapper
{
    public class JTokenToObjectListConverter : ITypeConverter<List<JToken>, List<object>>
    {
        public List<object> Convert(List<JToken> source, List<object> destination, ResolutionContext context)
        {
            var result = new List<object>();

            foreach (var item in source)
            {
                result.Add(item.ToObject<object>());
            }

            return result;
        }
    }
}
