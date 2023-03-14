using Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("[controller]")]
public class OrderController : Controller
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Index([FromRoute] int id)
    {
        var result = await mediator.Send(new GetOrderQuery { OrderId = id });

        return result.Match<IActionResult>(
            View,
            notFound => RedirectToAction("Index", "Main"));
    }
}