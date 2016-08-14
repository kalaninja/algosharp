using System.Linq;
using Xunit;

namespace Algosharp.Sorting.Tests.Helpers
{
    public static class AssertHelper
    {
        public static void IsSorted<T>(T[] actual)
        {
            var sorted = actual.OrderBy(x => x).ToArray();
            Assert.Equal(sorted, actual);
        }
    }
}
