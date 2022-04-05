using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> CreateAsync(CreateOrderDto OrderDto);
        Task<BaseResponse<Order>> GetAsync(Expression<Func<Order, bool>> expression);
        Task<BaseResponse<IEnumerable<Order>>> GetAllAsync(PaginationParams @params, Expression<Func<Order, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Order, bool>> expression);

    }
}
