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
        public OrderParser(string filePath) : base(filePath)
        {
        }
        public List<Order> GetOrders()
        {
            var delimitedByLine = GetDelimitedByLine();
            int increment = 1;
            var ordersList = delimitedByLine.Select(x =>
            {
                
                string[] delimitedByTab = x.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                return new Order()
                {
                    Id = increment++,
                    TimeReceived = DateTime.ParseExact(delimitedByTab[0], "yyyy-MM-ddTHH:mm:ss.fffffffZ", CultureInfo.InstalledUICulture),
                    Quantity = Convert.ToInt32(delimitedByTab[1]),
                    BrandId = Convert.ToInt32(delimitedByTab[2])
                };
            }).ToList();
            return ordersList;
        }


    }
}

