using System;

namespace Algosharp.Infrastructure.Benchmarking
{
    internal struct ActionLabeled
    {
        internal string Name { get; private set; }
        internal Action Action { get; private set; }

        public ActionLabeled(string name, Action action) : this()
        {
            Name = name;
            Action = action;
        }
    }
}
