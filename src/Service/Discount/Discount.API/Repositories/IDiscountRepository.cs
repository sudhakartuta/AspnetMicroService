using Discount.API.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupen> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupen coupen);
        Task<bool> UpdateDiscount(Coupen coupen);
        Task<bool> DeleteDiscount(string productName);
    }
}
