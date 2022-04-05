using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Interfaces
{
    public interface IContentService
    {
        Task<BaseResponse<Content>> CreateAsync(CreateContentDto ContentDto);
        Task<BaseResponse<IEnumerable<Content>>> GetAllAsync(PaginationParams @params, long studentId = 0);
        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Content, bool>> expression);
        Task<BaseResponse<Content>> UpdateAsync(long id, CreateContentDto ContentDto);

        Task<string> SaveFileAsync(Stream file, string fileName);
    }
}
