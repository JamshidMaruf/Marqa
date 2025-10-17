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
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpPost]
    public async Task<ActionResult<ProductViewModel>> Create([FromBody] ProductCreateModel dto)
    {
        try
        {
            var product = await _productService.CreateAsync(dto);
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
        var product = await _productService.UpdateAsync(id, dto);

        if (product == null)
            return NotFound();

        return Ok(product);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductViewModel>> Get(int id)
    {
        var product = await _productService.GetAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll([FromQuery] string search = null)
    {
        var products = await _productService.GetAllAsync(search);
        return Ok(products);
    }
}

