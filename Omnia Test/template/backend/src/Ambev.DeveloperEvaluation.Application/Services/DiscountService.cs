using Ambev.DeveloperEvaluation.Domain.DTOs;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    public class DiscountService : IDiscountService
    {
        public DiscountService() { }

        public DiscountResult Calculate(int quantity, decimal unitPrice)
        { 
            var discountPercentage = GetDiscountPercentage(quantity);

            var totalWithoutDiscount = quantity * unitPrice;
            var discountValue = totalWithoutDiscount * discountPercentage;
            var totalWithDiscount = totalWithoutDiscount - discountValue;

            return new DiscountResult
            {
                DiscountPercentage = discountPercentage,
                Total = totalWithDiscount
            };
        }

        private static decimal GetDiscountPercentage(int quantity)
        {
            if (quantity >= 10)
                return 0.20m;

            if (quantity >= 4)
                return 0.10m;

            return 0.0m;
        }
    }
}
