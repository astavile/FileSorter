using System;
using AltiumFileSorter.Wrappers.Interfaces;

namespace AltiumFileSorter.Wrappers
{
    public sealed class ConsoleProgressInformer : IProgressInformer
    {
        public void Inform(string message)
        {
            Console.WriteLine($"{DateTime.Now}: {message}");
        }

        public void SetProgress(long currentValue, long total)
        {
            Console.Write("{0:f2}%   \r", 100.0 * currentValue / total);
        }
    }
}
