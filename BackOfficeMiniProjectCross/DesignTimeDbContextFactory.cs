using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using BackOfficeMiniProject.DataAccess.Database.Context;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace BackOfficeMiniProjectCross
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BackOfficeDbContext>
//    {
//        public BackOfficeDbContext CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            var builder = new DbContextOptionsBuilder<BackOfficeDbContext>();

//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            //builder.UseMySql(connectionString);
//            builder.UseMySql("Server = localhost;port=3307; Database = BackOffice; User = root; Password = 12345;", b => b.MigrationsAssembly("BackOfficeMiniProject.DataAccess.Database"));
//            return new BackOfficeDbContext(builder.Options);
//        }
//    }
//}
