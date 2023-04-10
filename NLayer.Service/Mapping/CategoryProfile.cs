﻿using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDTO>();

        }
    }
}