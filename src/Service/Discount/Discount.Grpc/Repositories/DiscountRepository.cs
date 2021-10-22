using Discount.Grpc.Entites;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupen> GetDiscount(string productName)
        {
           
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupen = await connection.QueryFirstOrDefaultAsync<Coupen>("select * from Coupon where ProductName=@ProductName", new { ProductName = productName });
            if (coupen == null)
                return new Coupen { ProductName = "No Discount", Description = "No Discount desc", Amount = 0 };

            return coupen;
           
        }
        public async Task<bool> CreateDiscount(Coupen coupen)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRows = await connection.ExecuteAsync("insert into Coupon(ProductName,Description,Amount) values (@ProductName,@Description,@Amount)"
                                , new { @ProductName=coupen.ProductName, @Description=coupen.Description, @Amount=coupen.Amount });
            if (affectedRows == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateDiscount(Coupen coupen)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRows = await connection.ExecuteAsync("update Coupon set ProductName=@ProductName,Description=@Description,Amount=@Amount where Id=@Id"
                                                , new { @ProductName = coupen.ProductName, @Description = coupen.Description, @Amount = coupen.Amount,@Id=coupen.Id });
            if (affectedRows == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affectedRows = await connection.ExecuteAsync("delete Coupon where ProductName=@ProductName"
                                                , new { @ProductName = productName});

            if (affectedRows == 0)
                return false;

            return true;
        }    

       
    }
}
