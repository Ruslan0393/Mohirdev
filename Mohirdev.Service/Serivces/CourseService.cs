using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs;
using Mohirdev.Service.Extensions;
using Mohirdev.Service.Helpers;
using Mohirdev.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mohirdev.Service.Serivces
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Course>> CreateAsync(CreateCourseDto CourseDto)
        {
            var response = new BaseResponse<Course>();

            var resalt = await unitOfWork.User.GetAsync(p => p.Id == CourseDto.UserId);
            // check for exist Course
            if (resalt is null)
            {
                response.Error = new ErrorModel(404, "User must entered");
                return response;
            }
            // Check for state 
            if (resalt.State == State.Deleted)
            {
                response.Error = new ErrorModel(404, "User not found");
                return response;
            }
            // Check for role
            if (resalt.Role == Role.Student)
            {
                response.Error = new ErrorModel(402, "Student can't upload course");
                return response;
            }


            var mappedCourse = mapper.Map<Course>(CourseDto);

            mappedCourse.ImageName = await SaveFileAsync(CourseDto.Image.OpenReadStream(), CourseDto.Image.FileName);
            var result = await unitOfWork.Course.CreateAsync(mappedCourse);

            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + "://" + HttpContextHelper.Context?.Request?.Host.Value;
            string webUrl = $@"{hostUrl}/{config.GetSection("Storage:ImageUrl/").Value}";

            result.ImageName = webUrl + result.ImageName;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist Course
            var existCourse = await unitOfWork.Course.GetAsync(expression);
            if (existCourse is null)
            {
                response.Error = new ErrorModel(404, "Course not found");
                return response;
            }
            existCourse.Delete();

            await unitOfWork.Course.UpdateAsync(existCourse);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<IEnumerable<Course>>> GetAllAsync(PaginationParams @params, Expression<Func<Course, bool>> expression = null)
        {

            var response = new BaseResponse<IEnumerable<Course>>();

            var Courses = await unitOfWork.Course.GetAllAsync(expression);

            response.Data = Courses.ToPagedList(@params);

            return response;
        }

        public async Task<BaseResponse<Course>> GetAsync(Expression<Func<Course, bool>> expression)
        {
            var response = new BaseResponse<Course>();

            var Course = await unitOfWork.Course.GetAsync(expression);
            if (Course is null)
            {
                response.Error = new ErrorModel(404, "Course not found");
                return response;
            }

            response.Data = Course;

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

        public async Task<BaseResponse<Course>> UpdateAsync(long id, CreateCourseDto CourseDto)
        {
            var response = new BaseResponse<Course>();

            // check for exist Course
            var Course = await unitOfWork.Course.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (Course is null)
            {
                response.Error = new ErrorModel(404, "Course not found");
                return response;
            }

            Course.Name = CourseDto.Name;
            Course.Description = CourseDto.Description;
            Course.Price = CourseDto.Price;

            Course.ImageName = await SaveFileAsync(CourseDto.Image.OpenReadStream(), CourseDto.Image.FileName);
            Course.Update();

            var result = await unitOfWork.Course.UpdateAsync(Course);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
