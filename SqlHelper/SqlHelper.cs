using System.IO;
using Microsoft.Extensions.Configuration;

namespace ASP.NETCoreWebApplication
{
    public abstract class SqlHelper
    {
        public readonly string cnnString;
        
        public SqlHelper(string connectionString)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            cnnString = configuration.GetConnectionString(connectionString);
        }
    }
}