using Application.Orders.Requests;
using FluentValidation;

namespace Application.Validators;

public sealed class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Такого заказа не существует.");

        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("Поле Номер обязательно для заполнения.");

        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Поле Дата обязательно для заполнения.");

        RuleFor(x => x.ProviderId)
            .NotEmpty()
            .WithMessage("Поле Провайдер обязательно для заполнения.");

        RuleFor(x => x.OrderItems)
            .Must(x => x is not null && x.Any())
            .WithMessage("Необходим хотя-бы один элемент заказа.");
    }
}