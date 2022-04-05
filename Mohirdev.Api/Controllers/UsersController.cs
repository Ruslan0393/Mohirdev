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
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<User>>> Create([FromForm] CreateUserDto userDto)
        {
            var result = await userService.CreateAsync(userDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<User>>>> GetAll([FromQuery] PaginationParams @params)
        {
            var result = await userService.GetAllAsync(@params, p => p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Get([FromRoute] long id)
        {
            var result = await userService.GetAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpGet]
        [Route("login")]
        public async Task<ActionResult<BaseResponse<User>>> Login([FromQuery] string email, [FromQuery] string password)
        {
            var result = await userService.LoginAsync(email, password);
            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<User>>> Update(long id, [FromForm] CreateUserDto userDto)
        {
            var result = await userService.UpdateAsync(id, userDto);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }

        [HttpPut]
        [Route("update-balance/{id}")]
        public async Task<ActionResult<BaseResponse<User>>> UpdateBalance(long id, [FromQuery] decimal summa)
        {
            var result = await userService.UpdateBalanceAsync(id, summa);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(long id)
        {
            var result = await userService.DeleteAsync(p => p.Id == id && p.State != State.Deleted);

            return StatusCode(result.Error is null ? result.Code : result.Error.Code, result);
        }


    }
}
