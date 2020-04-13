using System;
using System.Collections.Generic;
using System.IO;
using BackOfficeMiniProject.DataAccess.Database.DataFileParsers;
using BackOfficeMiniProject.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeMiniProject.DataAccess.Database
{
    /// <summary>
    /// Extension that provide seed logic for database
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Provide name of Resources folder that contains tsv data files 
        /// </summary>
        public static string Resources = nameof(Resources);

        private static readonly string _brandFileName = "Brands.tsv";
        private static readonly string _orderFileName = "Brand_Quantity_Time_Received.tsv";

        /// <summary>
        /// Seed database, data loads from tsv files using parsers
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Seed(this ModelBuilder modelBuilder)
        {
            string resourceDirPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Substring(6) ?? throw new InvalidOperationException(), Resources);

            string brandFilePath = Path.Combine(resourceDirPath, _brandFileName);
            string orderFilePath = Path.Combine(resourceDirPath, _orderFileName);

            var brandPropertyToHeaderMap = new Dictionary<string, string> { { nameof(Brand.Id), "BRAND_ID" }, { nameof(Brand.Name), "Name" }};
            var brandParser = new BrandParser(brandFilePath,brandPropertyToHeaderMap);
            
            List<Brand> brands = brandParser.GetBrands();
            
            modelBuilder.Entity<Brand>().HasData(
                brands
            );

            var orderPropertyToHeaderMap = new Dictionary<string, string> { { nameof(Order.TimeReceived), "TIME_RECEIVED" }, { nameof(Order.Quantity), "QUANTITY" }, { nameof(Order.BrandId), "BRAND_ID" } };
            var orderParser = new OrderParser(orderFilePath, orderPropertyToHeaderMap);
            
            List<Order> orders = orderParser.GetOrders();

            modelBuilder.Entity<Order>().HasData(
                orders
            );
        }
    }
}

