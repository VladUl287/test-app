using Application.Orders.Queries;
using Application.Orders.Requests;
using Application.Providers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers;

[Route("[controller]/[action]")]
public class OrderManagerController : Controller
{
    private readonly IMediator mediator;

    public OrderManagerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var providers = await mediator.Send(new GetProvidersQuery());

        return View(providers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var providers = await mediator.Send(new GetProvidersQuery());
        var orderRequest = await mediator.Send(new GetOrderQuery() { OrderId = id });

        if (orderRequest.IsT1)
        {
            return RedirectToAction("Index", "Order", new { id });
        }

        var orderView = new UpdateOrder
        {
            Providers = providers.ToArray(),
            Order = orderRequest.AsT0
        };

        return View(orderView);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateOrderRequest createOrderRequest)
    {
        var result = await mediator.Send(createOrderRequest);

        return result.Match<IActionResult>(
            (success) => Ok(success),
            (error) => BadRequest(error));
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateOrderRequest createOrderRequest)
    {
        var result = await mediator.Send(createOrderRequest);

        return result.Match<IActionResult>(
            (success) => Ok(),
            (notFound) => NotFound("Ошибка удаления. Данный заказ не существует."));
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] int id)
    {
        await mediator.Send(new DeleteOrderRequest { OrderId = id });

        return RedirectToAction("Index", "Main");
    }
}