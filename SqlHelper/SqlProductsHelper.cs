using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ASP.NETCoreWebApplication
{
    public class ProductData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
    
    public interface ISqlProductsHelper
    {
        IEnumerable<ProductData> GetTopProducts();
        IEnumerable<ProductData> GetPopularProducts();
        IEnumerable<ProductData> GetActualProducts();
    }
    
    public class SqlProductsHelper : SqlHelper, ISqlProductsHelper
    {
        public SqlProductsHelper(string contextName) : base(contextName)
        {
        }
        
        private IEnumerable<ProductData> GetProductDataByQueryName(string queryName)
        {
            using var conn = new SqlConnection(cnnString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                conn.Close();
            }

            var cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "SelectProducts"
            };
            cmd.Parameters.Add("@queryName", SqlDbType.NVarChar);
            cmd.Parameters["@queryName"].Value = queryName;

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                yield return new ProductData()
                {
                    Id = (int)reader.GetValue(0),
                    Name = (string)reader.GetValue(1),
                    Price = (decimal)reader.GetValue(2),
                    Count = (int)reader.GetValue(3)
                };
            }

            conn.Close();
        }

        public IEnumerable<ProductData> GetTopProducts()
        {
            return GetProductDataByQueryName("top 5");
        }

        public IEnumerable<ProductData> GetPopularProducts()
        {
            return GetProductDataByQueryName("popular");
        }

        public IEnumerable<ProductData> GetActualProducts()
        {
            return GetProductDataByQueryName("actual");
        }
    }
}