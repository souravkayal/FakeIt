using AutoMapper;

namespace FakeIt.Common.Mapper
{
    public class DynamicToStringResolver<TSource> : IValueResolver<TSource, object, string>
    {
        private readonly Func<TSource, dynamic> _sourceMemberSelector;

        public DynamicToStringResolver(Func<TSource, dynamic> sourceMemberSelector)
        {
            _sourceMemberSelector = sourceMemberSelector;
        }

        public string Resolve(TSource source, object destination, string destMember, ResolutionContext context)
        {
            var sourceMember = _sourceMemberSelector(source);
            return System.Text.Json.JsonSerializer.Serialize(sourceMember);
        }
    }
}
