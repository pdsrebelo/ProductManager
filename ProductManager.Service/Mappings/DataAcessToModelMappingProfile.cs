using AutoMapper;
using ProductManager.Model.Entities;
using DataAcess = ProductManager.Data.Entities;
using Product = ProductManager.Model.Entities.Product;
using ProductCategory = ProductManager.Model.Entities.ProductCategory;

namespace ProductManager.Service.Mappings
{
    public class DataAcessToModelMappingProfile : Profile
    {
        public DataAcessToModelMappingProfile()
        {
            CreateMap<DataAcess.Product, Product>();
            CreateMap<DataAcess.ProductCategory, ProductCategory>();
            CreateMap<DataAcess.ProductSubcategory, ProductSubCategory>();
        }

        public override string ProfileName
        {
            get { return "DataAcessToModelMappingProfile"; }
        }
    }
}