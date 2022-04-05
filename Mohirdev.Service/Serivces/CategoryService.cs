using AutoMapper;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs.Category;
using Mohirdev.Service.Extensions;
using Mohirdev.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Serivces
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;


        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task<BaseResponse<Category>> CreateAsync(CreateCategoryDto CategoryDto)
        {
            var response = new BaseResponse<Category>();

            Category category = new Category();
            category.Name = CategoryDto.Name;




            var result = await unitOfWork.Category.CreateAsync(category);


            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Category, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist Category
            var existCategory = await unitOfWork.Category.GetAsync(expression);
            if (existCategory is null)
            {
                response.Error = new ErrorModel(404, "Category not found");
                return response;
            }

            await unitOfWork.Category.UpdateAsync(existCategory);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Category>>> GetAllAsync(PaginationParams @params, Expression<Func<Category, bool>> expression = null)
        {

            var response = new BaseResponse<IEnumerable<Category>>();

            var Categorys = await unitOfWork.Category.GetAllAsync(expression);

            response.Data = Categorys.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Category>> GetAsync(Expression<Func<Category, bool>> expression)
        {
            var response = new BaseResponse<Category>();

            var Category = await unitOfWork.Category.GetAsync(expression);
            if (Category is null)
            {
                response.Error = new ErrorModel(404, "Category not found");
                return response;
            }

            response.Data = Category;

            return response;
        }
    }
}
