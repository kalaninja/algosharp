using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Algosharp.Infrastructure.Benchmarking
{
    public static class Comparison
    {
        public static string GetComparison(this Result[] results)
        {
            Contract.Requires<ArgumentNullException>(results != null);
            Contract.Requires<ArgumentException>(results.Length > 0);

            var sb = new StringBuilder("Name\tMedian\tStdDev\n");
            foreach (var result in results.OrderBy(x => x.Median))
                sb.AppendLine($"{result.Name}\t{result.Median:###,###,###.#}\t{result.StdDev:0.##}");

            return sb.ToString();
        }
    }
}
