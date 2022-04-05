using Microsoft.AspNetCore.Mvc;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs.Content;
using Mohirdev.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mohirdev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private readonly IContentService ContentService;

        public ContentsController(IContentService ContentService)
        {
            this.ContentService = ContentService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Content>>> Create([FromForm] CreateContentDto ContentDto)
        {
            var result = await ContentService.CreateAsync(ContentDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Content>>>> GetAll([FromQuery] PaginationParams @params, long studentId)
        {
            var result = await ContentService.GetAllAsync(@params, studentId);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Content>>> Update(long id, [FromForm] CreateContentDto ContentDto)
        {
            var result = await ContentService.UpdateAsync(id, ContentDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await ContentService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }
    }
}
