using AfCoBRCore.Factory;
using AfCoBRCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AfCoBRCore.Controllers;

[ApiController]
[Route("api/[controller]")]
public partial class OrderController : Controller
{
  private readonly IConfiguration configuration;
  private readonly DiscountStrategyFactory discountStrategyFactory;

  public OrderController(IConfiguration configuration, DiscountStrategyFactory discountStrategyFactory)
  {
    this.configuration = configuration;
    this.discountStrategyFactory = discountStrategyFactory;
  }

  [HttpPost]
  public IActionResult CalculateOrderTotal([FromBody] OrderRequestModel orderRequest)
  {
    var customerId = orderRequest.CustomerId;
    var customerPreferences = configuration.GetSection($"CustomerPreferences:{customerId}").Get<CustomerPreferences>();

    var discountVersion = customerPreferences?.DiscountVersion ?? "DefaultVersion";
    var discountStrategy = discountStrategyFactory.GetDiscountStrategy(discountVersion);

    decimal discountAmount = discountStrategy.CalculateDiscount(orderRequest.TotalPrice);

    decimal totalPrice = orderRequest.TotalPrice - discountAmount;

    return Ok(new { DiscountAmount = discountAmount, TotalPrice = totalPrice });
  }
}
