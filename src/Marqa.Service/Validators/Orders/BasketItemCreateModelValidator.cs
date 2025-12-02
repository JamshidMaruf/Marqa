using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Orders.Models;

namespace Marqa.Service.Validators.Orders;
public class BasketItemCreateModelValidator : AbstractValidator<BasketItemCreateModel>
{
    public BasketItemCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.ProductId).Must(x => unitOfWork.Products.CheckExist(p => p.Id == x))
            .WithMessage("Product with the given ID does not exist.");
        RuleFor(x => x.Quantity).NotEmpty();
        RuleFor(x => x.InlinePrice).NotEmpty();
        RuleFor(x => x.BasketId).GreaterThan(0);
        RuleFor(x => x.BasketId).Must(x => unitOfWork.Baskets.CheckExist(b => b.Id == x))
            .WithMessage("Basket with the given ID does not exist.");
    }
}
