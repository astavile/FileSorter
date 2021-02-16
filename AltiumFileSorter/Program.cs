using System;
using AltiumFileSorter.FileSorters;
using AltiumFileSorter.FileSorters.Params;
using AltiumFileSorter.Wrappers;

namespace AltiumFileSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSorterParams = new FileSorterParams()
            {
                InputDataFileName = "TestData_1Gb.txt",
                MaxMemoryUsageInBytes = 2 * 1024L * 1024L * 1024L,
                OutputSortedFileName = "SortedTestData.txt",
                SplittedFileSizeInBytes = 50 * 1024L * 1024L,
                SplittedFilesDirectoryName = "TempData",
                ProgressInformer = new ConsoleProgressInformer()
            };

            var fileSorter = new FileSorter(fileSorterParams);
            var isSuccess = fileSorter.Sort();

            Console.WriteLine($"Is success sorting: {isSuccess}");
            Console.ReadLine();
        }
    }
}
