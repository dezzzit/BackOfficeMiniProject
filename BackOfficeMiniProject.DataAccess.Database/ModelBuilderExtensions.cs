﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BackOfficeMiniProject.DataAccess.Database.DataFileParsers;
using BackOfficeMiniProject.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeMiniProject.DataAccess.Database
{
    static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            const string brandFileName = "Brands.tsv";
            const string orderFileName = "Brand_Quantity_Time_Received.tsv";

            string resourceDirPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6) + "/Resources/";
            string brandFilePath = resourceDirPath + brandFileName;
            string orderFilePath = resourceDirPath + orderFileName;

            var brandParser = new BrandParser(brandFilePath);
            var brands = brandParser.GetBrands();
            modelBuilder.Entity<Brand>().HasData(
                brands
            );

            var orderParser = new OrderParser(orderFilePath);
            var orders = orderParser.GetOrders();

            modelBuilder.Entity<Order>().HasData(
                orders
            );
        }
    }
}

