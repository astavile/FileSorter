using System;

namespace AltiumFileSorter.Models
{
    public struct RowData
    {
        public RowData(string originalString)
        {
            this.originalString = originalString;
            var splittedData = originalString.Split(".", StringSplitOptions.RemoveEmptyEntries);

            Number = int.Parse(splittedData[0]);
            String = splittedData[1];
        }

        private string originalString;

        public int Number { get; }
        public string String { get; }

        public override string ToString() => originalString;
    }
}
