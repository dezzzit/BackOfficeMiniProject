using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    /// <summary>
    /// Base class for reading TSV files
    /// </summary>
    public abstract class Parser
    {
        private readonly string[] _fileDelimitedByLine;

        /// <summary>
        /// Initialization of reading TSV files 
        /// </summary>
        /// <param name="filePath">Full path to resource file</param>
        /// <param name="propertyToHeaderMap">Map file headers to property, key - property, value - header </param>
        protected Parser(string filePath, Dictionary<string,string> propertyToHeaderMap)
        {
            FilePath = filePath;
            var file = new FileInfo(FilePath);
            _fileDelimitedByLine = File.ReadAllText(file.FullName).Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            PropertyToHeaderMap = propertyToHeaderMap;
        }

        private string[] Headers
        {
            get
            {
                string[] headers = _fileDelimitedByLine[0]
                    .Split(new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim('"')).ToArray();
                return headers;
            }
        }

        private string FilePath { get; }

        /// <summary>
        /// Gets mapping file headers to property, key - property, value - header 
        /// </summary>
        protected Dictionary<string, string> PropertyToHeaderMap { get; }

        /// <summary>
        /// Provide line array of tsv file
        /// </summary>
        /// <returns>Lines array</returns>
        public string[] GetDelimitedDataByLine()
        {
           return _fileDelimitedByLine.Skip(1).ToArray();
        }
        /// <summary>
        /// Provide position of pointed header
        /// </summary>
        /// <param name="propertyName">Class property name that was mapped to tsv header</param>
        /// <returns>Header's position in tsv file </returns>
        protected int GetHeaderIndex(string propertyName)
        {
            return Array.FindIndex(Headers,r=>r==PropertyToHeaderMap[propertyName]);
        }

       
    }
}
