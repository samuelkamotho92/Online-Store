using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Dtos;
using Online_Store.Models;
using Online_Store.Services.IService;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        private readonly IMapper _mapper;
        public ProductController(IProductService product, IMapper mapper) {
            _productService = product;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<Products>>>  GetProducts()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpGet]
        public async Task <ActionResult<Products>> GetProduct(Guid id)
        {
            Products product =  await  _productService.GetProductAsync(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddProduct(AddProductDto addproduct)
        {
            //imapper for mapping
         var newProduct = _mapper.Map<Products>(addproduct);
         string resp =   await _productService.AddProduct(newProduct);
            return Ok(resp);
        }
        [HttpPut]
        public async Task<ActionResult<string>> UpdateProduct(Guid id,AddProductDto updatedProd)
        {
         var product =  await  _productService.GetProductAsync(id);

            if (product != null)
            {
                var newProd = _mapper.Map<Products>(updatedProd);
               return await  _productService.UpdateProduct(newProd);
            }
            return "something went wrong";
        }
        public async Task<ActionResult<string>> DeleteProduct(Guid id)
        {
            var product = await _productService.GetProductAsync(id);
            var productDlt = _mapper.Map<Products>(product);
            return await _productService.DeleteProduct(productDlt);
        }
    }
}
