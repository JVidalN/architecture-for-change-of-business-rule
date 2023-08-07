using AfCoBRCore.Factory;
using Microsoft.AspNetCore.Mvc;

namespace AfCoBRCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiscountController : ControllerBase
{
  private readonly DiscountStrategyFactory discountStrategyFactory;

  public DiscountController(DiscountStrategyFactory discountStrategyFactory)
  {
    this.discountStrategyFactory = discountStrategyFactory;
  }

  [HttpGet("{customerId}/{totalPrice}")]
  public IActionResult CalculateDiscount(string customerId, decimal totalPrice)
  {
    var customerDiscountVersion = GetCustomerDiscountVersion(customerId);

    var discountStrategy = discountStrategyFactory.GetDiscountStrategy(customerDiscountVersion);

    decimal discountAmount = discountStrategy.CalculateDiscount(totalPrice);

    return Ok(new { DiscountAmount = discountAmount });
  }

  private string GetCustomerDiscountVersion(string customerId) => customerId switch
  {
    "Customer1" => "V1",
    "Customer2" => "V2",
    _ => "DefaultVersion",
  };
}
