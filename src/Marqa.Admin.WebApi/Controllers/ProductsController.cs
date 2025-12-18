using Marqa.Service.Services.Products;
using Marqa.Service.Services.Products.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ProductCreateModel model)
    {
        await productService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] ProductUpdateModel model)
    {
        await productService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await productService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await productService.GetAsync(id);

        return Ok(new Response<ProductViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = product,
        });
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(
        int companyId,
        [FromQuery] PaginationParams @params,
        [FromQuery] string? search = null)
    {
        var products = await productService.GetAllAsync(companyId, search);
        
        return Ok(new Response<List<ProductViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = products,
        });
    }
}

