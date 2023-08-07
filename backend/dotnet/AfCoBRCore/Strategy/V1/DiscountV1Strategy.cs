using AfCoBRCore.Interfaces;

namespace AfCoBRCore.Strategy;

public class DiscountV1Strategy : IDiscountStrategy
{
  public decimal CalculateDiscount(decimal totalPrice)
  {
    // Implement the logic for Discount V1
    return totalPrice * 0.1M; // Example: 10% discount
  }
}
