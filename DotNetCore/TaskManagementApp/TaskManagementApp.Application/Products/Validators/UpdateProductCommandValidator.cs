using FluentValidation;
using TaskManagementApp.Application.Products.Commands;

namespace TaskManagementApp.Application.Products.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Description).NotEmpty();
            RuleFor(t => t.Price > 0);
            RuleFor(t => t.Quantity > 0);
        }
    }
}
