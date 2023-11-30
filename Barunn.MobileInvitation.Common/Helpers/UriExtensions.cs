using System;
using System.Linq;

namespace Barunn.MobileInvitation.Common.Helpers
{
    public static class UriExtensions
    {
        public static Uri Combine(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('\\', '/'), path.TrimStart('\\', '/'))));
        }
    }
}
