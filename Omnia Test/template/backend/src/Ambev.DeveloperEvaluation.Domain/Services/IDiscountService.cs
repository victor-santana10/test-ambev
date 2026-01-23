using Ambev.DeveloperEvaluation.Domain.DTOs;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public interface IDiscountService
    {
        DiscountResult Calculate(int quantity, decimal unitPrice);
    }
}
