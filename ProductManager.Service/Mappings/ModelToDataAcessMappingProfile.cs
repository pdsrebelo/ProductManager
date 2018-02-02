using AutoMapper;
using ProductManager.Model.Entities;
using DataAcess = ProductManager.Data.Entities;
using Product = ProductManager.Model.Entities.Product;
using ProductCategory = ProductManager.Model.Entities.ProductCategory;

namespace ProductManager.Service.Mappings
{
    public class ModelToDataAcessMappingProfile : Profile
    {
        public ModelToDataAcessMappingProfile()
        {
            CreateMap<Product, DataAcess.Product>();
            CreateMap<ProductCategory, DataAcess.ProductCategory>();
            CreateMap<ProductSubCategory, DataAcess.ProductSubcategory>();
        }

        public override string ProfileName
        {
            get { return "ModelToDataAcessMappingProfile"; }
        }
    }
}