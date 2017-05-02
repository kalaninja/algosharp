using System;

namespace Algosharp.Infrastructure.Benchmarking
{
	internal struct ActionLabeled
	{
		internal string Name { get; }
		internal Action Action { get; }

		public ActionLabeled(string name, Action action) : this()
		{
			Name = name;
			Action = action;
		}
	}
}
