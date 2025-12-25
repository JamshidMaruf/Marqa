using Marqa.Service.Services.Products;
using Marqa.Service.Services.Products.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class ProductsController(IProductService productService) : BaseController
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

    [HttpGet("{id:int}/update")]
    [ProducesResponseType(typeof(Response<ProductViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUpdateFormDataAsync(int id)
    {
        var product = await productService.GetForUpdateAsync(id);

        return Ok(new Response<ProductUpdateFormModel>
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
        var products = await productService.GetAllAsync(companyId, @params, search);
        
        return Ok(new Response<List<ProductTableModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = products,
        });
    }

    [HttpPatch("{id:int}/upload-image")]
    public async Task<IActionResult> UploadImageAsync(int id, IFormFile image)
    {
        await productService.UploadPictureAsync(id, image);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Image uploaded successfully"
        });
    }

    [HttpDelete("{id:int}/remove-image")]
    public async Task<IActionResult> UploadImageAsync(int id, int imageId)
    {
        await productService.RemoveImageAsync(id, imageId);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Image removed successfully"
        });
    }
}

