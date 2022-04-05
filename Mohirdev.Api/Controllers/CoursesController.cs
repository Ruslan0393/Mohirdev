using Microsoft.AspNetCore.Mvc;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs;
using Mohirdev.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mohirdev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Course>>> Create([FromForm] CreateCourseDto CourseDto)
        {
            var result = await courseService.CreateAsync(CourseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Course>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await courseService.GetAllAsync(@params, p => p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Get([FromRoute] long id)
        {
            var result = await courseService.GetAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Course>>> Update(long id, [FromForm] CreateCourseDto CourseDto)
        {
            var result = await courseService.UpdateAsync(id, CourseDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await courseService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
