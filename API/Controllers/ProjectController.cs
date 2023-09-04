using Microsoft.AspNetCore.Mvc;
using TaskBoard.Authorization.API.Controllers;

namespace API.Controllers;

public class ProductController: ApiBaseController
{
    private readonly IProductService _productService;


    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> CreateAsync(ProductRequest product)
    {
        ArgumentNullException.ThrowIfNull(product);
        await _productService.CreateAsync(product, new CancellationToken());
        return Ok();
    }
    [HttpPut(Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateAsync(ProductRequest product)
    {
        ArgumentNullException.ThrowIfNull(product);
        await _productService.UpdateAsync(product, new CancellationToken());
        return Ok();
    }
    [HttpGet(Name = "GetAllProduct")]
    public async Task<List<Models.Product>> GetAll()
    {
        var listProduct = await _productService.GetAllAsync(new CancellationToken());
        return listProduct;
    }
    [HttpGet("{id}")]
    public async Task<Models.Product> GetById(Guid id)
    {
        var product = await _productService.GetByIdAsync(id, new CancellationToken());
        return product;
    }
}
