using Dapper;
using EcommerceMonolithic.Models;
using System.Data.SqlClient;

namespace EcommerceMonolithic.DataAccess
{
    public class ProductProvider : IProductProvider
    {
        private readonly string _connectionString;

        public ProductProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Product[] Get()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Product>(@"SELECT Id, Name, Description, Type FROM Product")
                .ToArray();
        }
    }
}
