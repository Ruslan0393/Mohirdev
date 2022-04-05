using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Interfaces
{
    public interface ICourseService
    {
        Task<BaseResponse<Course>> CreateAsync(CreateCourseDto courseDto);
        Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression);
        Task<BaseResponse<Course>> UpdateAsync(long id, CreateCourseDto courseDto);

        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
