using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.Service.Services.Products.Models;
using Marqa.Service.Services.Products;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Marqa.Service.Services.EmployeeRoles;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductCreateModel model)
    {
        await productService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 201,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateModel dto)
    {
        await productService.UpdateAsync(id, dto);

        return Ok(new Response
        {
            Status = 201,
            Message = "success"
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await productService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 201,
            Message = "success"
        });
    }

    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var product = await productService.GetAsync(id);

        return Ok(new Response<ProductViewModel>
        {
            Status = 200,
            Message = "success",
            Data = product,
        });
    }

    [HttpGet("getAll/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId, [FromQuery] string search = null)
    {
        var products = await productService.GetAllAsync(companyId, search);
        
        return Ok(new Response<List<ProductViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = products,
        });
    }
}

