using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs.Content;
using Mohirdev.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohirdev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService ContentService;

        public ContentController(IContentService ContentService)
        {
            this.ContentService = ContentService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Content>>> Create([FromForm] CreateContentDto ContentDto)
        {
            var result = await ContentService.CreateAsync(ContentDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<Content>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await ContentService.GetAllAsync(@params, p => p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<Content>>> Get([FromRoute] long id)
        {
            var result = await ContentService.GetAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<Content>>> Update(long id, [FromForm] CreateContentDto ContentDto)
        {
            var result = await ContentService.UpdateAsync(id, ContentDto);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await ContentService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Code ?? result.Error.Code.Value, result);
        }
    }
}
