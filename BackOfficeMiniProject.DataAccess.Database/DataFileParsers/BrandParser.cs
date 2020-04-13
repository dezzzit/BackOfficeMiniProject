using System;
using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    /// <summary>
    /// Contains logic for reading Brands TSV files
    /// </summary>
    public class BrandParser : Parser
    {
        /// <summary>
        /// Initialize brands parser
        /// </summary>
        /// <param name="filePath">Full path to brands file</param>
        /// <param name="propertyToHeaderMap">Map file headers to property, key - property, value - header</param>
        public BrandParser(string filePath, Dictionary<string, string> propertyToHeaderMap) : base(filePath, propertyToHeaderMap)
        {

        }

        /// <summary>
        ///  Returns brands list of items 
        /// </summary>
        /// <returns> Brands list</returns>
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
