using Marqa.Service.Services.Product.Models;
using Marqa.Service.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
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

