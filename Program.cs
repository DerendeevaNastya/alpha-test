using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ASP.NETCoreWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TestHelpers();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        // проверка работоспособности хелперов
        public static void TestHelpers()
        {
            var helper = new SqlProductsHelper("ProductsContext");
            var productDatas = helper.GetTopProducts().ToArray();
            var hel = new SqlUserHelper("UsersContext");
            var kek = hel.AddRole(
                new ClientRole
                {
                    ClientId = 1,
                    RoleName = "Аналитик"
                });
            
            Console.WriteLine(productDatas.Length);
            Console.WriteLine(kek);
        }
    }
}