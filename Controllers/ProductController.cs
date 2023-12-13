using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Dtos;
using Online_Store.Models;
using Online_Store.Services.IService;
using System.Net;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ResponseDto _responseDto;

        private readonly IProductService _productService;

        private readonly IMapper _mapper;
        public ProductController(IProductService product, IMapper mapper)
        {
            _productService = product;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ActionResult<List<Products>>>  GetProducts()
        {
            try
            {
                _responseDto.Result = await _productService.GetProductsAsync();
                _responseDto.message = "Success";
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.message = $"Something went very wrong: {ex.InnerException}";
                return NotFound(_responseDto);
            }
           
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<Products>> GetProduct(Guid id)
        {
            try
            {
                
                Products product = await _productService.GetProductAsync(id);
                Console.WriteLine(product);
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.message = "success";
                _responseDto.Result = product;
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.message = $"Failure {ex.InnerException}";
                return NotFound(_responseDto);
            }
          
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddProduct(AddProductDto addproduct)
        {
            try
            {
                //imapper for mapping
                var newProduct = _mapper.Map<Products>(addproduct);
                string resp = await _productService.AddProduct(newProduct);
                _responseDto.message = resp;
                _responseDto.Result = newProduct;
                _responseDto.StatusCode = HttpStatusCode.Created;
               
                return Ok(_responseDto);
             
            }
            catch(Exception e)
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }
         
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid id,AddProductDto updatedProd)
        {
            try {
                var product = await _productService.GetProductAsync(id);

                if (product != null)
                {
                    var newProd = _mapper.Map(updatedProd,product);
                    string resp = await _productService.UpdateProduct(newProd);
                    _responseDto.Result = newProd;
                    _responseDto.message = resp;
                    return Ok(_responseDto);

                }
                _responseDto.message = "Not found";
                return BadRequest(_responseDto);
            }
            catch(Exception e){
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }         
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProductAsync(id);
                var productDlt = _mapper.Map<Products>(product);
                string resp = await _productService.DeleteProduct(productDlt);
                _responseDto.Result = null;
                _responseDto.message = resp;
                _responseDto.StatusCode=HttpStatusCode.NoContent;
                return Ok(_responseDto);
            }
            catch(Exception e)
            {
                _responseDto.message = $" Something is wrong {e.InnerException}";
                return BadRequest(_responseDto);

            }
           
        }
    }
}
