using AutoMapper;
using ProductManager.Model.Entities;
using ProductManager.WebApi.Models;

namespace ProductManager.WebApi.Mappings
{
    internal class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductCategory, ProductCategoryModel>();
            CreateMap<ProductSubCategory, ProductSubCategoryModel>();
        }

        public override string ProfileName
        {
            get { return "DomainToModelMappings"; }
        }
    }
}