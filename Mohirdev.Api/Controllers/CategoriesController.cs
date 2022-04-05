using Microsoft.AspNetCore.Mvc;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs.Category;
using Mohirdev.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mohirdev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Category>>> Create([FromForm] CreateCategoryDto CategoryDto)
        {
            var result = await categoryService.CreateAsync(CategoryDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Category>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await categoryService.GetAllAsync(@params, p => p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Category>>> Get([FromRoute] long id)
        {
            var result = await categoryService.GetAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await categoryService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
