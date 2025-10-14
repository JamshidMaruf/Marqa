using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.Service.Services.Product.Models;
using Marqa.Service.Services.Product;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Marqa.Service.Services.Products;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService productService;
    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }
    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> Create([FromBody] ProductCreateModel dto)
    {
        try
        {
            var product = await productService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<ProductViewModel>> Update(int id, [FromBody] ProductUpdateModel dto)
    {
        var product = await productService.UpdateAsync(id, dto);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> Get(int id)
    {
        var product = await productService.GetAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll([FromQuery] string search = null)
    {
        var products = await productService.GetAllAsync(search);
        return Ok(products);
    }
}

