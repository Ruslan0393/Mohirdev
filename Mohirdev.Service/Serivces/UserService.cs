using Microsoft.Extensions.Configuration;
using Mohirdev.Domain.Configurations;
using Microsoft.AspNetCore.Hosting;
using Mohirdev.Service.Interfaces;
using Mohirdev.Data.IRepositories;
using Mohirdev.Service.Extensions;
using System.Collections.Generic;
using Mohirdev.Service.Helpers;
using Mohirdev.Domain.Entities;
using System.Linq.Expressions;
using Mohirdev.Domain.Commons;
using System.Threading.Tasks;
using Mohirdev.Service.DTOs;
using Mohirdev.Domain.Enums;
using AutoMapper;
using System.IO;
using System;

namespace Mohirdev.Service.Serivces
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<User>> CreateAsync(CreateUserDto UserDto)
        {
            var response = new BaseResponse<User>();

            var existUser = await unitOfWork.User.GetAsync(p => p.Email == UserDto.Email);
            if (existUser is not null)
            {
                response.Error = new ErrorModel(400, "User is exist");
                return response;
            }

            // create after checking success
            var mappedUser = mapper.Map<User>(UserDto);

            // save image from dto model to wwwroot
            mappedUser.ImageName = await SaveFileAsync(UserDto.ImageName.OpenReadStream(), UserDto.ImageName.FileName);

            var result = await unitOfWork.User.CreateAsync(mappedUser);

            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + "://" + HttpContextHelper.Context?.Request?.Host.Value;
            string webUrl = $@"{hostUrl}/{config.GetSection("File:Organization").Value}";

            result.ImageName = webUrl + result.ImageName;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist User
            var existUser = await unitOfWork.User.GetAsync(expression);
            if (existUser is null)
            {
                response.Error = new ErrorModel(404, "User not found");
                return response;
            }
            existUser.Delete();

            var result = await unitOfWork.User.UpdateAsync(existUser);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<User>>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null)
        {

            var response = new BaseResponse<IEnumerable<User>>();

            var Users = await unitOfWork.User.GetAllAsync(expression);

            response.Data = Users.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            var User = await unitOfWork.User.GetAsync(expression);
            if (User is null)
            {
                response.Error = new ErrorModel(404, "User not found");
                return response;
            }

            response.Data = User;

            return response;
        }

        public async Task<BaseResponse<User>> LoginAsync(string email, string password)
        {
            var response = new BaseResponse<User>();

            var User = await unitOfWork.User.GetAsync(p => p.Email == email && p.Password == password && p.State != State.Deleted);
            if (User is null)
            {
                response.Error = new ErrorModel(404, "Login or password wrong");
                return response;
            }


            response.Data = User;

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:ImageUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return fileName;
        }

        public async Task<BaseResponse<User>> UpdateAsync(long id, CreateUserDto UserDto)
        {
            var response = new BaseResponse<User>();

            var User = await unitOfWork.User.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (User is null)
            {
                response.Error = new ErrorModel(404, "User not found");
                return response;
            }

            User.FirstName = UserDto.FirstName;
            User.LastName = UserDto.LastName;
            User.PhoneNumber = UserDto.PhoneNumber;
            User.Email = UserDto.Email;
            User.Password = UserDto.Password;
            User.Balance = UserDto.Balance;
            User.ImageName = await SaveFileAsync(UserDto.ImageName.OpenReadStream(), UserDto.ImageName.FileName);
            User.Update();

            var result = await unitOfWork.User.UpdateAsync(User);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<User>> UpdateBalanceAsync(long id, decimal summa)
        {
            var response = new BaseResponse<User>();

            var User = await unitOfWork.User.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (User is null)
            {
                response.Error = new ErrorModel(404, "User not found");
                return response;
            }

            User.Balance += summa;
            User.Update();

            var result = await unitOfWork.User.UpdateAsync(User);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
