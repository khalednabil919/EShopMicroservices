using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;
public record UpdateOrderCommand(OrderDto OrderDto):ICommand<UpdateOrderResult>;
public record UpdateOrderResult(bool IsSuccess);
public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto.Id).NotEmpty().WithMessage("Id is Required");
        RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("Name is Required");
        RuleFor(x => x.OrderDto.CustomerId).NotNull().WithMessage("CustomerId is Required");
    }
}
