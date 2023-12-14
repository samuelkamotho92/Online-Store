using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Dtos;
using Online_Store.Models;
using Online_Store.Services;
using Online_Store.Services.IService;
using System.Net;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private readonly IOrder _orderService;
        private readonly IMapper _mapper;

        //create an instance
        public OrderController(IOrder orderService, IMapper mapper) { 
        
            _orderService = orderService;
            _mapper = mapper;
            _responseDto = new ResponseDto();
        }
        [HttpGet]
        public async Task<ActionResult<List<ResponseDto>>> GetOrders()
        {
            try
            {
                _responseDto.Result = await _orderService.GetOrdersAsync();
                _responseDto.message = "Success";
                return Ok(_responseDto);

            }catch(Exception e)
            {
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }

        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddOrder(Guid id,AddOrderDto addOrderDto)
        {
            try
            {
                //maping 
                var order = _mapper.Map<Order>(addOrderDto);
                order.productId = id;
                string resp =await  _orderService.AddOrder(order);
                _responseDto.message = resp;
                _responseDto.StatusCode = HttpStatusCode.Created;
                _responseDto.Result = order;
                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetOrderById(Guid id)
        {
            try
            {
                Order order =  await  _orderService.GetOrderAsync(id);
                _responseDto.message = "success";
                _responseDto.Result = order;
                return Ok(_responseDto);
            }catch(Exception e)
            {
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> UpdateOrder(Guid id, AddOrderDto updatedOrder)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);

                if (order != null)
                {
                    var newOrder = _mapper.Map(updatedOrder, order);
                    string resp = await _orderService.UpdateOrder(newOrder);
                    _responseDto.Result = newOrder;
                    _responseDto.message = resp;
                    return Ok(_responseDto);
                }
                _responseDto.message = "Not found";
                return BadRequest(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.message = $"failure {e.InnerException}";
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> deleteOrder(Guid id)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(id);
                var orderDlt = _mapper.Map<Order>(order);
               string resp =await  _orderService.DeleteOrder(orderDlt);
                _responseDto.message = resp;
                _responseDto.StatusCode = HttpStatusCode.NoContent;
                _responseDto.Result = null;
                return Ok(_responseDto);
            }
            catch (Exception e)
            {
                _responseDto.message = $"{e.InnerException}";
                return BadRequest(_responseDto);
            }
        }

    }
}
