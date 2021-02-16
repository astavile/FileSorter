using System;
using System.Collections.Generic;
using AltiumFileSorter.Models;

namespace AltiumFileSorter.DataSorters
{
    public class RowDataSorter : IComparer<RowData>
    {
        public int Compare(RowData x, RowData y)
        {
            var compareStringsResult = string.Compare(x.String, y.String);
            if (compareStringsResult != 0)
                return compareStringsResult;

            return x.Number - y.Number;
        }
    }
}
