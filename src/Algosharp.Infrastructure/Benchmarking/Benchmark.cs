﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algosharp.Infrastructure.Benchmarking
{
	public partial class Benchmark
	{
		private readonly List<ActionLabeled> _actions = new List<ActionLabeled>();

		private int _warmup = 1;

		private Benchmark(string name, Action action)
		{
			_actions.Add(new ActionLabeled(name, action));
		}

		public Benchmark And(string name, Action action)
		{
			_actions.Add(new ActionLabeled(name, action));
			return this;
		}

		public Benchmark Warmup(int times)
		{
			if (times < 1) throw new ArgumentException($"{nameof(times)} is less than 1", nameof(times));

			_warmup = times;
			return this;
		}

		public Result[] Run(int times)
		{
			if (times < 1) throw new ArgumentException($"{nameof(times)} is less than 1", nameof(times));

			var results = new Result[_actions.Count];

			for (var i = 0; i < _actions.Count; i++)
			{
				var sw = new Stopwatch();
				var action = _actions[i].Action;
				var result = new Result(_actions[i].Name, sw);

				for (var j = 0; j < _warmup; j++)
					action();

				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();

				sw.Start();
				for (var j = 0; j < times; j++)
				{
					action();

					result.AddTickStamp(sw.ElapsedTicks);
				}

				sw.Stop();
				results[i] = result;
			}

			return results;
		}

		public Result[] RunFor(TimeSpan time)
		{
			if (time.Ticks < 1) throw new ArgumentException($"{nameof(time.Ticks)} is less than 1", nameof(time.Ticks));

			var results = new Result[_actions.Count];

			for (var i = 0; i < _actions.Count; i++)
			{
				var sw = new Stopwatch();
				var action = _actions[i].Action;
				var result = new Result(_actions[i].Name, sw);

				for (var j = 0; j < _warmup; j++)
					action();

				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();

				sw.Start();
				while (sw.Elapsed.Ticks <= time.Ticks)
				{
					action();

					result.AddTickStamp(sw.ElapsedTicks);
				}

				sw.Stop();
				results[i] = result;
			}

			return results;
		}

		public static Benchmark For(string name, Action action)
		{
			return new Benchmark(name, action);
		}
	}
}
