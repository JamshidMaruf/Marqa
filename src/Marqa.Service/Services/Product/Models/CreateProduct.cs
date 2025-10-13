using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Product.Models
{
    public class CreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }
    }
}
