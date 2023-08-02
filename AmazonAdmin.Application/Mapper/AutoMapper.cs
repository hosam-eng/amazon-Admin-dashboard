using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, SubCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ShowProductDTO>().ReverseMap();
            CreateMap<Product,AddUpdateProductDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserRegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserLoginDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemShow>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<AddUpdateProductDTO, ShowProductDTO>().ReverseMap();
			CreateMap<Task<List<Image>>, List<ImageDTO>>();
			CreateMap<Category, AddCategoryDto>().ReverseMap();
			CreateMap<CategoryDTO, AddCategoryDto>().ReverseMap();
			CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<City, CitiesListDTO>().ReverseMap();
            CreateMap<City, CreateUpdateCity>().ReverseMap();
            CreateMap<CitiesListDTO, CreateUpdateCity>().ReverseMap();
            CreateMap<shippingAddress, AddAndEditShippingAddressDTO>().ReverseMap();
        }
    }
}
