using AfCoBRCore.Interfaces;

namespace AfCoBRCore.Strategy;

public class DiscountV2Strategy : IDiscountStrategy
{
  public decimal CalculateDiscount(decimal totalPrice)
  {
    // Implement the logic for Discount V2
    return totalPrice * 0.15M; // Example: 15% discount
  }
}
