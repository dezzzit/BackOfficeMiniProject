using BackOfficeMiniProject.DataAccess.Database.Test.AppSettings;
using Microsoft.Extensions.Configuration;

namespace BackOfficeMiniProject.DataAccess.Database.Test
{
    public class Settings
    {
        static Settings()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appSettings.json", optional: false);

            IConfigurationRoot configuration = builder.Build();

            ConnectionString = configuration.GetSection(nameof(ConnectionString))
                .Get<ConnectionString>();
        }

        public static ConnectionString ConnectionString { get; }
    }
}