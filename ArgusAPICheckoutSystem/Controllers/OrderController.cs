using ArgusAPICheckoutSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ArgusAPICheckoutSystem.Support;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private static Dictionary<int, Order> orders = new Dictionary<int, Order>();
    private static int orderCounter = 1;

    // 📌 1️⃣ Create a new order
    [HttpPost("book")]
    public IActionResult BookOrder([FromBody] OrderRequest request)
    {
        int newOrderId = orderCounter++;

        var newOrder = new Order
        {
            OrderId = newOrderId,
            People = request.People,
            Starters = request.Starters,
            Mains = request.Mains,
            Drinks = Helpers.ConvertTimetoFloat(request.Hour) < 19 ? 0 : request.Drinks,
            DrinksWithDiscount = Helpers.ConvertTimetoFloat(request.Hour) < 19 ? request.Drinks : 0
        };

        orders[newOrderId] = newOrder;
        return Created("", new { orderId = newOrderId, people = request.People });
    }

    // 📌 2️⃣ Add items to an existing order
    [HttpPost("add/{orderId}")]
    public IActionResult AddToOrder(int orderId, [FromBody] OrderRequest request)
    {
        if (!orders.ContainsKey(orderId))
            return NotFound(new { message = "Order not found" });

        var existingOrder = orders[orderId];

        existingOrder.People += request.People;
        existingOrder.Starters += request.Starters;
        existingOrder.Mains += request.Mains;

        if (Helpers.ConvertTimetoFloat(request.Hour) < 19)
            existingOrder.DrinksWithDiscount += request.Drinks;
        else
            existingOrder.Drinks += request.Drinks;

        return Ok(new { orderId, people = existingOrder.People });
    }

    // 📌 3️⃣ Remove items from an order
    [HttpDelete("delete/{orderId}")]
    public IActionResult RemoveFromOrder(int orderId, [FromBody] OrderRequest request)
    {
        if (!orders.ContainsKey(orderId))
            return NotFound(new { message = "Order not found" });

        var existingOrder = orders[orderId];

        existingOrder.People = Math.Max(0, existingOrder.People - request.People);
        existingOrder.Starters = Math.Max(0, existingOrder.Starters - request.Starters);
        existingOrder.Mains = Math.Max(0, existingOrder.Mains - request.Mains);

        if (Helpers.ConvertTimetoFloat(request.Hour) < 19)
            existingOrder.DrinksWithDiscount = Math.Max(0, existingOrder.DrinksWithDiscount - request.Drinks);
        else
            existingOrder.Drinks = Math.Max(0, existingOrder.Drinks - request.Drinks);

        return Ok(new { orderId, people = existingOrder.People });
    }

    // 📌 4️⃣ Calculate and return the bill
    [HttpGet("checkout/bill/{orderId}")]
    public IActionResult GetBill(int orderId)
    {
        if (!orders.ContainsKey(orderId))
            return NotFound(new { message = "Order not found" });

        var order = orders[orderId];

        // Prices
        decimal starterPrice = 4.00m;
        decimal mainPrice = 7.00m;
        decimal drinkPrice = 2.50m;
        decimal discountRate = 0.30m; // 30% discount

        // Drink discount calculation
        decimal drinksWithDiscountTotal = order.DrinksWithDiscount * drinkPrice * (1 - discountRate);
        decimal drinksFullPriceTotal = order.Drinks * drinkPrice;

        // Total Calculation
        decimal total = ((order.Starters * starterPrice) + (order.Mains * mainPrice)) * 1.1m // 10% service charge
                        + drinksWithDiscountTotal + drinksFullPriceTotal;

        return Ok(new { totalAmount = $"£{total:F2}", people = order.People });
    }
}

public class OrderRequest
{
    public int People { get; set; }
    public int Starters { get; set; }
    public int Mains { get; set; }
    public int Drinks { get; set; }
    public string Hour { get; set; }
}
