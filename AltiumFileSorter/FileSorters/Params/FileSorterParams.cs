using AltiumFileSorter.Wrappers.Interfaces;

namespace AltiumFileSorter.FileSorters.Params
{
    public sealed class FileSorterParams
    {
        /// <summary>
        /// Max memory usage (in bytes) for sorting work
        /// </summary>
        public long MaxMemoryUsageInBytes { get; set; }

        /// <summary>
        /// Size of splitted files (in bytes)
        /// </summary>
        public long SplittedFileSizeInBytes { get; set; }

        /// <summary>
        /// The name of directory, which would keep splitted files
        /// </summary>
        public string SplittedFilesDirectoryName { get; set; }

        /// <summary>
        /// Input file name with data
        /// </summary>
        public string InputDataFileName { get; set; }

        /// <summary>
        /// Output file name with sorted data
        /// </summary>
        public string OutputSortedFileName { get; set; }

        /// <summary>
        /// Informer for sending messages about sorting process
        /// </summary>
        public IProgressInformer ProgressInformer { get; set; }

        /// <summary>
        /// Check is parameters fields are valid
        /// </summary>
        /// <returns>Validation boolean result</returns>
        public bool IsValid() =>
            MaxMemoryUsageInBytes > 0 && MaxMemoryUsageInBytes < 64 * 1024L * 1024L * 1024L &&
            SplittedFileSizeInBytes > 0 && SplittedFileSizeInBytes < 100 * 1024L * 1024L * 1024L &&
            !string.IsNullOrWhiteSpace(SplittedFilesDirectoryName) &&
            !string.IsNullOrWhiteSpace(InputDataFileName) &&
            !string.IsNullOrWhiteSpace(OutputSortedFileName) &&
            ProgressInformer != null;

    }
}
