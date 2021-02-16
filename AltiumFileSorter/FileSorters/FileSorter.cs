using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using AltiumFileSorter.Models;
using AltiumFileSorter.DataSorters;
using AltiumFileSorter.FileSorters.Params;
using System.Threading.Tasks;

namespace AltiumFileSorter.FileSorters
{
    public class FileSorter
    {
        public FileSorter(FileSorterParams parameters)
        {
            this.parameters = parameters;
        }

        private readonly FileSorterParams parameters;

        public bool Sort()
        {
            return ExecuteActionWithDiagnostic(() => 
            {
                if (!parameters.IsValid())
                    return false;

                // SplitInputFile();
                var totalRecordsCount = SortChunks();
                MergeChunks(totalRecordsCount);

                return true;
            });
        }

        private bool ExecuteActionWithDiagnostic(Func<bool> function)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var isSuccess = function();

            stopWatch.Stop();

            if (isSuccess)
                parameters.ProgressInformer.Inform($"Finish of execute! Elapsed time range: {stopWatch.Elapsed}");
            else
                parameters.ProgressInformer?.Inform($"Error of execution!");

            return isSuccess;
        }

        private void SplitInputFile()
        {
            if (Directory.Exists(parameters.SplittedFilesDirectoryName))
            {
                Directory.Delete(parameters.SplittedFilesDirectoryName, true);
                Directory.CreateDirectory(parameters.SplittedFilesDirectoryName);
            }

            int splitFileIndex = 1;
            long redLinesCount = 0;
            var splittedFileWriter = new StreamWriter(GetSplitFileName(splitFileIndex));

            parameters.ProgressInformer.Inform($"Start splitting input file {parameters.InputDataFileName}...");
            using (StreamReader sr = new StreamReader(parameters.InputDataFileName))
            {
                while (sr.Peek() >= 0)
                {
                    splittedFileWriter.WriteLine(sr.ReadLine());

                    if (++redLinesCount % 5000 == 0)
                        parameters.ProgressInformer.SetProgress(sr.BaseStream.Position, sr.BaseStream.Length);

                    if (splittedFileWriter.BaseStream.Length > parameters.SplittedFileSizeInBytes && sr.Peek() >= 0)
                    {
                        splittedFileWriter.Close();
                        splittedFileWriter = new StreamWriter(GetSplitFileName(++splitFileIndex));
                    }
                }
            }
            parameters.ProgressInformer.Inform("Finish splitting input file...");

            splittedFileWriter.Dispose();
        }

        private long SortChunks()
        {
            var splittedFilePaths = Directory.GetFiles(parameters.SplittedFilesDirectoryName, "split*.dat");
            var stringSorter = new StringSorter();
            long totalRecordsCount = 0;

            //var parallelOptions = new ParallelOptions()
            //{
            //    MaxDegreeOfParallelism = (int)(parameters.MaxMemoryUsageInBytes / parameters.SplittedFileSizeInBytes / 10)
            //};

            // Parallel.ForEach(splittedFilePaths, (splitFilePath) => 

            foreach (var splitFilePath in splittedFilePaths)
            {
                var dataStrings = File.ReadAllLines(splitFilePath);
                totalRecordsCount += dataStrings.Length;

                parameters.ProgressInformer.Inform($"Start sort spliited file '{splitFilePath}'");
                Array.Sort(dataStrings, stringSorter);
                parameters.ProgressInformer.Inform($"Finish sort spliited file '{splitFilePath}'");

                var sortedFilePath = splitFilePath.Replace("split", "sorted");
                File.WriteAllLines(sortedFilePath, dataStrings);
                File.Delete(splitFilePath);

                GC.Collect();
            }

            return totalRecordsCount;
        }

        private void MergeChunks(long totalRecordsCount)
        {
            const double recordOverhead = 7.5;
            var paths = Directory.GetFiles(parameters.SplittedFilesDirectoryName, "sorted*.dat");

            var chunksCount = paths.Length;
            var averageRecordSize = 20;
            var buffersize = parameters.MaxMemoryUsageInBytes / chunksCount;
            var bufferlen = (int)(buffersize / averageRecordSize / recordOverhead);

            var readers = new StreamReader[chunksCount];
            var queues = new Queue<RowData>[chunksCount];
            parameters.ProgressInformer.Inform("Start of merging...");

            parameters.ProgressInformer.Inform("Preparing queues for merging files...");
            for (var i = 0; i < chunksCount; i++)
            {
                var currentReader = new StreamReader(paths[i]);
                var currentQueue = new Queue<RowData>(bufferlen);

                LoadQueue(currentQueue, currentReader, bufferlen);

                queues[i] = currentQueue;
                readers[i] = currentReader;
            }
            parameters.ProgressInformer.Inform("Finish preparing queues for merging files...");

            var outputFileWriter = new StreamWriter(parameters.OutputSortedFileName);

            var rowDataSorter = new RowDataSorter();
            int lowestIndex, currentProgress = 0;
            RowData lowestValue;

            while (true)
            {
                if (++currentProgress % 5000 == 0)
                    parameters.ProgressInformer.SetProgress(currentProgress, totalRecordsCount);

                lowestIndex = -1;
                lowestValue = new RowData();

                for (var i = 0; i < chunksCount; i++)
                {
                    var currentQueue = queues[i];
                    if (currentQueue != null)
                    {
                        if (lowestIndex < 0 || rowDataSorter.Compare(currentQueue.Peek(), lowestValue) < 0)
                        {
                            lowestIndex = i;
                            lowestValue = currentQueue.Peek();
                        }
                    }
                }

                if (lowestIndex == -1)
                    break;

                outputFileWriter.WriteLine(lowestValue);

                var lowestQueue = queues[lowestIndex];
                lowestQueue.Dequeue();

                if (lowestQueue.Count == 0)
                {
                    LoadQueue(lowestQueue, readers[lowestIndex], bufferlen);

                    if (lowestQueue.Count == 0)
                        queues[lowestIndex] = null;
                }
            }

            outputFileWriter.Close();

            parameters.ProgressInformer.Inform("Deleting queues after merging files...");
            for (var i = 0; i < chunksCount; i++)
            {
                readers[i].Close();
                File.Delete(paths[i]);
            }

            parameters.ProgressInformer.Inform("Finish of merging!");
        }

        private void LoadQueue(Queue<RowData> queue, StreamReader file, int records)
        {
            for (int i = 0; i < records; i++)
            {
                if (file.Peek() < 0)
                    break;

                queue.Enqueue(new RowData(file.ReadLine()));
            }
        }

        private string GetSplitFileName(int fileIndex)
        {
            return Path.Combine(parameters.SplittedFilesDirectoryName, $"split{fileIndex:d5}.dat");
        }
    }
}
