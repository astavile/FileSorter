using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AltiumFileGenerator.Services.Interfaces
{
    public interface IFileGenerator
    {
        /// <summary>
        /// Create file in app directory with specific size (Gb)
        /// </summary>
        /// <param name="fileSizeInGb">The file size in Gb</param>
        void GenerateFile(ushort fileSizeInGb);

        /// <summary>
        /// Event, when the part of data was written to the file
        /// </summary>
        event EventHandler OnDataPartCreated;
    }
}
