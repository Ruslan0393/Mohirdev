using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs;
using Mohirdev.Service.DTOs.Order;
using Mohirdev.Service.Extensions;
using Mohirdev.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Serivces
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<Order>> CreateAsync(CreateOrderDto OrderDto)
        {
            var response = new BaseResponse<Order>();

            var res = await unitOfWork.Course.GetAsync(p => p.Id == OrderDto.CourseId);
            var user = await unitOfWork.User.GetAsync(p => p.Id == OrderDto.ClientId);
            if(res is null)
            {
                response.Error = new ErrorModel(400, $"{res.Name} course not found");
                return response;
            }

            var course = await unitOfWork.StudentCourses.GetAsync(p => p.UserId == OrderDto.ClientId && p.CourseId == OrderDto.CourseId);
            
            if(course is not null)
            {
                response.Error = new ErrorModel(400, $"This course already bought");
                return response;
            }

            if(user.Balance <= res.Price)
            {
                response.Error = new ErrorModel(400, $"Your need {res.Price - user.Balance}$ for buy ");
                return response;
            }

            user.Balance = user.Balance - res.Price;
            res.Price = user.Balance + res.Price;

            var mappedOrder = mapper.Map<Order>(OrderDto);
            var mappedStudentCourses = mapper.Map<StudentCourses>(new StudentCoursesDto { CourseId = res.Id, UserId = user.Id });

            await unitOfWork.User.UpdateAsync(user);
            await unitOfWork.Course.UpdateAsync(res);
            await unitOfWork.StudentCourses.CreateAsync(mappedStudentCourses);
            var result = await unitOfWork.Order.CreateAsync(mappedOrder);


            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist Order
            var existOrder = await unitOfWork.Order.GetAsync(expression);
            if (existOrder is null)
            {
                response.Error = new ErrorModel(404, "Order not found");
                return response;
            }

            await unitOfWork.Order.UpdateAsync(existOrder);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Order>>> GetAllAsync(PaginationParams @params, Expression<Func<Order, bool>> expression = null)
        {

            var response = new BaseResponse<IEnumerable<Order>>();

            var Orders = await unitOfWork.Order.GetAllAsync(expression);

            response.Data = Orders.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Order>> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var response = new BaseResponse<Order>();

            var Order = await unitOfWork.Order.GetAsync(expression);
            if (Order is null)
            {
                response.Error = new ErrorModel(404, "Order not found");
                return response;
            }

            response.Data = Order;

            return response;
        }

       
    }
}
