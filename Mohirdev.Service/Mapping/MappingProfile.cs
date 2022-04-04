using AutoMapper;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs;
using Mohirdev.Service.DTOs.Content;
using Mohirdev.Service.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohirdev.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<CreateContentDto, Content>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<StudentCoursesDto, StudentCourses>().ReverseMap();
        }
    }
}
