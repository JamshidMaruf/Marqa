using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Orders.Models;

namespace Marqa.Service.Validators.Orders;
public class BasketItemCreateModelValidator : AbstractValidator<BasketItemCreateModel>
{
    public BasketItemCreateModelValidator()
    {
        RuleFor(x => x.ProductId).GreaterThan(0);
        RuleFor(x => x.Quantity).NotEmpty();
        RuleFor(x => x.InlinePrice).NotEmpty();
        RuleFor(x => x.BasketId).GreaterThan(0);
    }
}
