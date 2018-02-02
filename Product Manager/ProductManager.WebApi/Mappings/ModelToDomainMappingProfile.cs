using AutoMapper;
using ProductManager.Model.Entities;
using ProductManager.WebApi.Models;

namespace ProductManager.WebApi.Mappings
{
    internal class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<ProductCategoryModel, ProductCategory>();
            CreateMap<ProductSubCategoryModel, ProductSubCategory>();
        }

        public override string ProfileName
        {
            get { return "ModelToDomainMappings"; }
        }
    }
}