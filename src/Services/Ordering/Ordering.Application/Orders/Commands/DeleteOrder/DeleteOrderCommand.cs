using FluentValidation;
namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid id):ICommand<DeleteOrderResult>;
public record DeleteOrderResult(bool IsSuccess);
public class DeleteOrderCommandValidation:AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidation()
    {
        RuleFor(x => x.id).NotEmpty().WithMessage("OrderId is Required");
    }
}