using System;
using System.Collections.Generic;
using AltiumFileSorter.Models;

namespace AltiumFileSorter.DataSorters
{
    public class StringSorter : IComparer<string>
    {
        private static RowDataSorter rowDataSorter = new RowDataSorter();

        public int Compare(string x, string y)
        {
            var xRowData = new RowData(x);
            var yRowData = new RowData(y);

            return rowDataSorter.Compare(xRowData, yRowData);
        }
    }
}
