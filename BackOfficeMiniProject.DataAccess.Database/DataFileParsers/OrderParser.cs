using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Database.DataFileParsers
{
    public class OrderParser : Parser
    {
        public OrderParser(string filePath, Dictionary<string, string> propertyToHeaderMap ) : base(filePath,propertyToHeaderMap)
        {
           
        }
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

