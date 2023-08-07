namespace AfCoBRCore.Interfaces;

public interface IDiscountStrategy
{
  decimal CalculateDiscount(decimal totalPrice);
}
