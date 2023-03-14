using Application.Orders.Queries;
using Application.Providers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers;

public class MainController : Controller
{
    private readonly IMediator mediator;

    public MainController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var query = new GetOrdersQuery
        {
            PeriodStart = DateTime.Now.AddMonths(-1),
            PeriodEnd = DateTime.Now
        };
        var orders = await mediator.Send(query);
        var providers = await mediator.Send(new GetProvidersQuery());

        var mainOrders = new MainOrders
        {
            Filters = query,
            Orders = orders,
            OrdersNumbers = orders,
            Providers = providers
        };

        return View(mainOrders);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] GetOrdersQuery getOrders)
    {
        var filteredOrders = await mediator.Send(getOrders);
        var orders = await mediator.Send(new GetOrdersQuery());
        var providers = await mediator.Send(new GetProvidersQuery());

        var mainOrders = new MainOrders
        {
            Filters = getOrders,
            Orders = filteredOrders,
            OrdersNumbers = orders,
            Providers = providers
        };

        return View(mainOrders);
    }
}