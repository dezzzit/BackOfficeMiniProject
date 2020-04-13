using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    /// <summary>
    /// Contains logic for reading orders TSV files
    /// </summary>
    public class OrderParser : Parser
    {
        /// <summary>
        /// Initialize orders parser
        /// </summary>
        /// <param name="filePath">Full path to orders file</param>
        /// <param name="propertyToHeaderMap">Map file headers to property, key - property, value - header</param>
        public OrderParser(string filePath, Dictionary<string, string> propertyToHeaderMap ) : base(filePath,propertyToHeaderMap)
        {
           
        }

        /// <summary>
        /// Returns orders list of items 
        /// </summary>
        /// <returns>Orders list</returns>
        public List<Order> GetOrders()
        {
            var delimitedByLine = GetDelimitedDataByLine();
            int increment = 1;
            var ordersList = delimitedByLine.Select(x =>
            {
                string[] delimitedByTab = x.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                
                return new Order()
                {
                    Id = increment++,
                    TimeReceived = DateTime.ParseExact(delimitedByTab[GetHeaderIndex(nameof(Order.TimeReceived))], "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InstalledUICulture),
                    Quantity = Convert.ToInt32(delimitedByTab[GetHeaderIndex(nameof(Order.Quantity))]),
                    BrandId = Convert.ToInt32(delimitedByTab[GetHeaderIndex(nameof(Order.BrandId))])
                };
            }).ToList();

            return ordersList;
        }
    }
}

