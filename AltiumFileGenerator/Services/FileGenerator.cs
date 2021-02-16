using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AltiumFileGenerator.Services.Interfaces;

namespace AltiumFileGenerator.Services
{
    public sealed class FileGenerator : IFileGenerator
    {
        public FileGenerator(int iterationsCount)
        {
            this.iterationsCount = iterationsCount;
        }

        private const string OutputFileName = "TestData.txt";
        private readonly int iterationsCount;

        public event EventHandler OnDataPartCreated;

        public void GenerateFile(ushort fileSizeInGb)
        {
            if (fileSizeInGb <= 0)
                return;

            if (File.Exists(OutputFileName))
                File.Delete(OutputFileName);

            long fileSizeInBytes = fileSizeInGb * 1024L * 1024L * 1024L;

            using (var writeStream = File.OpenWrite(OutputFileName))
            {
                writeStream.SetLength(fileSizeInBytes);
                AddRowsToFile(fileSizeInBytes, writeStream);
            }
        }

        private void AddRowsToFile(long fileSizeInBytes, FileStream fileStream)
        {
            var onePartLimit = fileSizeInBytes / iterationsCount;
            var random = new Random();
            var fileWriteLocker = new object();
            var duplicateStringsCount = random.Next(1, 10);

            Parallel.For(0, iterationsCount, (iteration) =>
            {
                var stringBuilder = new StringBuilder();
                var maxLimit = Math.Min(stringBuilder.MaxCapacity, onePartLimit);

                TryAddDuplicateDataRow(stringBuilder, random, ref duplicateStringsCount);
                while (stringBuilder.Length < maxLimit)
                {
                    stringBuilder.AppendLine(GetRandomDataRow(random));
                }

                var dataBytes = Encoding.Default.GetBytes(stringBuilder.ToString());
                lock (fileWriteLocker)
                {
                    fileStream.Write(dataBytes, 0, dataBytes.Length);
                }

                RaiseOnDataPartCreated();
            });
        }

        private void TryAddDuplicateDataRow(StringBuilder stringBuilder, Random random, ref int duplicateStringsCount)
        {
            if (Interlocked.Decrement(ref duplicateStringsCount) > 0)
            {
                var duplicateDataRow = GetRandomDataRow(random);

                stringBuilder.AppendLine(duplicateDataRow);
                stringBuilder.AppendLine(duplicateDataRow);
            }
        }

        private string GetRandomDataRow(Random random)
        {
            var randomNumer = random.Next(1, 1000);
            var stringPartLength = random.Next(5, 20);

            var randomString = GetRandomString(stringPartLength);
            return string.Concat(randomNumer.ToString(), ".", randomString);
        }

        private string GetRandomString(int length)
        {
            return Guid.NewGuid().ToString("N").Substring(0, length);
        }

        private void RaiseOnDataPartCreated()
        {
            if (OnDataPartCreated != null)
                OnDataPartCreated(this, new EventArgs());
        }
    }
}
