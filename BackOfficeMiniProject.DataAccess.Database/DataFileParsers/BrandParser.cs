using System;
using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    public class BrandParser: Parser
    {
        public BrandParser(string filePath) : base(filePath)
        {
        }
        public List<Brand> GetBrands()
        {
            var delimitedByLine = GetDelimitedByLine();
            var brandsList = delimitedByLine.Select(x =>
            {
                string[] delimitedByTab = x.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                return new Brand()
                {
                    Id = Convert.ToInt32(delimitedByTab[0]),
                    Name = delimitedByTab[1].Trim('"')
                };
            }).ToList();
            return brandsList;
        }


    }
}
