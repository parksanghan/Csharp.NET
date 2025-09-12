using System;
using System.Windows.Markup;

namespace DXApplication4.Infrastructure
{
    public class DISource : MarkupExtension
    {
        public static Func<Type?, object>? Resolver { get; set; }
        public Type? Type { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider) => Resolver?.Invoke(Type)!;
    }
}
