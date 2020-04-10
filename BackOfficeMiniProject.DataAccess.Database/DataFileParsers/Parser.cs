using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace BackOfficeMiniProject.DataAccess.Database
{
    public abstract class Parser
    {
        private string[] _fileDelimitedByLine;

        /// <summary>
        /// 
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

        public string[] GetDelimitedDataByLine()
        {
           return _fileDelimitedByLine.Skip(1).ToArray();
        }

        protected int GetHeaderIndex(string propertyName)
        {
            return Array.FindIndex(Headers,r=>r==PropertyToHeaderMap[propertyName]);
        }

        protected string FilePath { get; }
        /// <summary>
        /// Gets mapping file headers to property, key - property, value - header 
        /// </summary>
        protected Dictionary<string,string> PropertyToHeaderMap { get; }
       
    }
}
