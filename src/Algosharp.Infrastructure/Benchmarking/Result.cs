using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Algosharp.Infrastructure.Extensions;

namespace Algosharp.Infrastructure.Benchmarking
{
    public class Result
    {
        private readonly List<long> _tickStamps = new List<long>();
        private readonly Stopwatch _stopwatch;
        private readonly Lazy<List<long>> _tickSpans;
        private readonly Lazy<double> _median;
        private readonly Lazy<double> _variance;

        public string Name { get; }

        public int TotalIterations => TickStamps.Count;

        public TimeSpan TotalTime => _stopwatch.Elapsed;

        public IReadOnlyCollection<long> TickStamps => _tickStamps;

        public IReadOnlyCollection<long> TickSpans => _tickSpans.Value;

        internal Result(string name, Stopwatch stopwatch)
        {
            Name = name;
            _stopwatch = stopwatch;

            _tickSpans =
                new Lazy<List<long>>(
                    () => TickStamps.DiffSelect((curr, prev, first) => first ? curr : curr - prev).ToList());

            _median = new Lazy<double>(() =>
            {
                var spans = TickSpans.OrderBy(x => x);
                var midpoint = TotalIterations / 2;
                return TotalIterations % 2 == 0
                    ? (spans.ElementAt(midpoint - 1) + spans.ElementAt(midpoint)) / 2d
                    : spans.ElementAt(midpoint);
            });

            _variance = new Lazy<double>(() =>
            {
                var avg = Average;
                var sum = TickSpans.Sum(x => (x - avg) * (x - avg));
                return sum / TotalIterations;
            });
        }

        public double Average => _stopwatch.ElapsedTicks / (double)TotalIterations;

        public double Median => _median.Value;

        public double Variance => _variance.Value;

        public double StdDev => Math.Sqrt(Variance);

        internal void AddTickStamp(long tickstamp)
        {
            _tickStamps.Add(tickstamp);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"-----{Name}-----");

            if (TotalIterations == 0)
            {
                sb.AppendLine("No benchmark conducted");
                return sb.ToString();
            }

            sb.AppendLine($"Total time: {TotalTime}");
            sb.AppendLine($"Ticks per execute (avg): {Average}");
            sb.AppendLine($"Ticks per execute (median): {Median}");
            sb.AppendLine($"Milliseconds per execute: {_stopwatch.ElapsedMilliseconds / TotalIterations}");

            return sb.ToString();
        }
    }
}
