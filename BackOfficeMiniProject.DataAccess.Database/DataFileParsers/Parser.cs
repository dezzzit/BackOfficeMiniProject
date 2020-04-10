using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BackOfficeMiniProject.DataAccess.Database
{
    public abstract class Parser
    {
        protected Parser(string filePath)
        {
            FilePath = filePath;
        }

        public string[] GetDelimitedByLine()
        {
            var file = new FileInfo(FilePath);
            string[] delimitedByLine = File.ReadAllText(file.FullName).Split(new string[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            delimitedByLine = delimitedByLine.Skip(1).ToArray();
            return delimitedByLine;
        }

        protected string FilePath { get; }
    }
}
