using System;
using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    public class BrandParser : Parser
    {
        public BrandParser(string filePath, Dictionary<string, string> propertyToHeaderMap) : base(filePath, propertyToHeaderMap)
        {

        }

        public List<Brand> GetBrands()
        {
            var delimitedByLine = GetDelimitedDataByLine();
            var brandsList = delimitedByLine.Select(x =>
            {
                string[] delimitedByTab = x.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                
                return new Brand()
                {
                    Id = Convert.ToInt32(delimitedByTab[GetHeaderIndex(nameof(Brand.Id))]),
                    Name = delimitedByTab[GetHeaderIndex(nameof(Brand.Name))].Trim('"')
                };
            }).ToList();

            return brandsList;
        }
    }
}
