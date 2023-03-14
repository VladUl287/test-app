using Application;
using Domain.Repositories;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Presentation.Extensions;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseInMemoryDatabase(databaseName: "TestDb");
        //options.UseSqlServer(
        //    builder.Configuration.GetConnectionString("DatabaseConnection"),
        //    options => options.MigrationsAssembly(typeof(IInfrastructureMarker).Assembly.FullName));
    });

    builder.Services.AddMediatR<IApplicationMarker>();

    builder.Services.AddValidatorsFromAssembly(typeof(IApplicationMarker).Assembly);

    builder.Services.AddMapper<IApplicationMarker>();

    builder.Services.AddScoped<IOrderRepository, OrderRepsitory>();
    builder.Services.AddScoped<IProviderRepository, ProviderRepsoitory>();
}

var app = builder.Build();
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Main}/{action=Index}");

    app.Run();
}