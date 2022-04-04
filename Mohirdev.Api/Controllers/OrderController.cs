using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs.Order;
using Mohirdev.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohirdev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService OrderService;

        public OrderController(IOrderService OrderService)
        {
            this.OrderService = OrderService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Order>>> Create([FromForm] CreateOrderDto OrderDto)
        {
            var result = await OrderService.CreateAsync(OrderDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Order>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await OrderService.GetAllAsync(@params, p => p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Order>>> Get([FromRoute] long id)
        {
            var result = await OrderService.GetAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await OrderService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
