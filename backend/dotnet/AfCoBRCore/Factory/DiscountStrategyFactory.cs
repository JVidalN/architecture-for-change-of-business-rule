using AfCoBRCore.Interfaces;
using AfCoBRCore.Strategy;

namespace AfCoBRCore.Factory;

public class DiscountStrategyFactory
{
  private readonly Dictionary<string, IDiscountStrategy> discountStrategies;

  public DiscountStrategyFactory()
  {
    discountStrategies = new Dictionary<string, IDiscountStrategy>
    {
      { "V1", new DiscountV1Strategy() },
      { "V2", new DiscountV2Strategy() },
    };
  }

  public IDiscountStrategy GetDiscountStrategy(string version) => discountStrategies.ContainsKey(version) ? discountStrategies[version] : discountStrategies["V1"];
}
