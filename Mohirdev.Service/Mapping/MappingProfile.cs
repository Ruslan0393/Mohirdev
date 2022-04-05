using AutoMapper;
using Mohirdev.Domain.Entities;
using Mohirdev.Service.DTOs;
using Mohirdev.Service.DTOs.Category;
using Mohirdev.Service.DTOs.Content;
using Mohirdev.Service.DTOs.Order;

namespace Mohirdev.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<CreateOrderDto, Order>().ReverseMap();
            CreateMap<CreateCourseDto, Course>().ReverseMap();
            CreateMap<CreateContentDto, Content>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<StudentCoursesDto, StudentCourses>().ReverseMap();
        }
    }
}
