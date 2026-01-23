using Ambev.DeveloperEvaluation.Application.Services;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _service;

        public DiscountServiceTests()
        {
            _service = new DiscountService();
        }

        [Fact]
        public void Calculate_Should_Return_No_Discount_When_Quantity_Less_Than_4()
        {
            var quantity = 2;
            var unitPrice = 10m;

            var result = _service.Calculate(quantity, unitPrice);

            Assert.Equal(0.0m, result.DiscountPercentage);
            Assert.Equal(20m, result.Total);
        }

        [Fact]
        public void Calculate_Should_Return_10_Percent_Discount_When_Quantity_Is_4_Or_More()
        {
            var quantity = 5;
            var unitPrice = 10m;

            var result = _service.Calculate(quantity, unitPrice);

            Assert.Equal(0.10m, result.DiscountPercentage);
            Assert.Equal(45m, result.Total);
        }

        [Fact]
        public void Calculate_Should_Return_20_Percent_Discount_When_Quantity_Is_10_Or_More()
        {
            var quantity = 10;
            var unitPrice = 10m;

            var result = _service.Calculate(quantity, unitPrice);

            Assert.Equal(0.20m, result.DiscountPercentage);
            Assert.Equal(80m, result.Total);
        }

        [Fact]
        public void Calculate_Should_Handle_Zero_Quantity()
        {
            var quantity = 0;
            var unitPrice = 10m;

            var result = _service.Calculate(quantity, unitPrice);

            Assert.Equal(0.0m, result.DiscountPercentage);
            Assert.Equal(0m, result.Total);
        }
    }
}
