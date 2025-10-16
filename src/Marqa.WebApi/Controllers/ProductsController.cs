<<<<<<< HEAD
﻿using Marqa.Service.Services.Product.Models;
using Marqa.Service.Services.Products;
=======
﻿using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.Service.Services.Products.Models;
using Marqa.Service.Services.Products;
using Marqa.WebApi.Models;
>>>>>>> e5c696dbdfa0f966bffc15b8a85fcd1f41526ada
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
<<<<<<< HEAD
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
=======
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
>>>>>>> e5c696dbdfa0f966bffc15b8a85fcd1f41526ada
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
<<<<<<< HEAD
        var product = await productService.GetAsync(id);
=======
        await productService.DeleteAsync(id);
>>>>>>> e5c696dbdfa0f966bffc15b8a85fcd1f41526ada

        return Ok(new Response
        {
            Status = 201,
            Message = "success"
        });
    }

    [HttpGet("getById/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
<<<<<<< HEAD
        var products = await productService.GetAllAsync(search);
        return Ok(products);
=======
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
>>>>>>> e5c696dbdfa0f966bffc15b8a85fcd1f41526ada
    }
}

