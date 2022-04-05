using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Mohirdev.Data.IRepositories;
using Mohirdev.Domain.Commons;
using Mohirdev.Domain.Configurations;
using Mohirdev.Domain.Entities;
using Mohirdev.Domain.Enums;
using Mohirdev.Service.DTOs.Content;
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
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        public ContentService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env, IConfiguration config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.env = env;
            this.config = config;
        }

        public async Task<BaseResponse<Content>> CreateAsync(CreateContentDto ContentDto)
        {
            var response = new BaseResponse<Content>();

            var resaltUser = await unitOfWork.Course.GetAsync(p => p.Id == ContentDto.CourseId);
            if (resaltUser is null)
            {
                response.Error = new ErrorModel(404, "This is course not exist");
                return response;
            }
            if (resaltUser.State == State.Deleted)
            {
                response.Error = new ErrorModel(404, "Course not found");
                return response;
            }


            var mappedContent = mapper.Map<Content>(ContentDto);


            mappedContent.VideoUrl = await SaveFileAsync(ContentDto.Video.OpenReadStream(), ContentDto.Video.FileName);
            var result = await unitOfWork.Content.CreateAsync(mappedContent);
            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + "://" + HttpContextHelper.Context?.Request?.Host.Value;
            string webUrl = $@"{hostUrl}/{config.GetSection("File:Organization").Value}";
            result.VideoUrl = webUrl + result.VideoUrl;

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Content, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            // check for exist Content
            var existContent = await unitOfWork.Content.GetAsync(expression);
            if (existContent is null)
            {
                response.Error = new ErrorModel(404, "Content not found");
                return response;
            }
            existContent.Delete();

            var result = await unitOfWork.Content.UpdateAsync(existContent);

            await unitOfWork.SaveChangesAsync();

            response.Data = true;

            return response;
        }



        public async Task<BaseResponse<IEnumerable<Content>>> GetAllAsync(PaginationParams @params, long studentId = 0)
        {
            var response = new BaseResponse<IEnumerable<Content>>();

            var student = await unitOfWork.StudentCourses.GetAsync(p => p.UserId == studentId);

            if (student is null)
            {
                response.Error = new ErrorModel(400, "Student not found");
                return response;
            }

            var Contents = await unitOfWork.Content.GetAllAsync(p => p.CourseId == student.CourseId && p.State != State.Deleted);

            response.Data = Contents.ToPagedList(@params);

            return response;
        }

        public async Task<string> SaveFileAsync(Stream file, string fileName)
        {
            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;
            string storagePath = config.GetSection("Storage:VideoUrl").Value;
            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);
            await file.CopyToAsync(mainFile);
            mainFile.Close();
            return fileName;
        }

        public async Task<BaseResponse<Content>> UpdateAsync(long id, CreateContentDto ContentDto)
        {
            var response = new BaseResponse<Content>();

            // check for exist Content
            var Content = await unitOfWork.Content.GetAsync(p => p.Id == id && p.State != State.Deleted);
            if (Content is null)
            {
                response.Error = new ErrorModel(404, "Content not found");
                return response;
            }

            Content.Name = ContentDto.Name;
            Content.ExtraInfo = ContentDto.ExtraInfo;
            Content.Update();

            var result = await unitOfWork.Content.UpdateAsync(Content);

            await unitOfWork.SaveChangesAsync();

            response.Data = result;

            return response;
        }
    }
}
