using System;

namespace AltiumFileSorter.Wrappers.Interfaces
{
    public interface IProgressInformer
    {
        /// <summary>
        /// Shows the current progress of work
        /// </summary>
        /// <param name="currentValue">Current progress value</param>
        /// <param name="total">Total value</param>
        void SetProgress(long currentValue, long total);

        /// <summary>
        /// Inform about message
        /// </summary>
        /// <param name="message">Message</param>
        void Inform(string message);
    }
}
