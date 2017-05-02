using System;
using System.Linq;
using System.Text;

namespace Algosharp.Infrastructure.Benchmarking
{
	public static class Comparison
	{
		public static string GetComparison(this Result[] results)
		{
			if (results == null) throw new ArgumentNullException(nameof(results));
			if (results.Length == 0) throw new ArgumentException($"{nameof(results)} is empty", nameof(results));

			var sb = new StringBuilder("Name\tMedian\tStdDev\n");
			foreach (var result in results.OrderBy(x => x.Median))
			{
				sb.AppendLine($"{result.Name}\t{result.Median:###,###,###.#}\t{result.StdDev:0.##}");
			}

			return sb.ToString();
		}
	}
}
