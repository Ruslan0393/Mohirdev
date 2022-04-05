using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<Category>> CreateAsync(CreateCategoryDto categoryDto);
        Task<BaseResponse<Category>> GetAsync(Expression<Func<Category, bool>> expression);
        Task<BaseResponse<IEnumerable<Category>>> GetAllAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Category, bool>> expression);
    }
}
