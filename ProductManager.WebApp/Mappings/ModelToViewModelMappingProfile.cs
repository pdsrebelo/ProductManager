using AutoMapper;
using ProductManager.WebApi.Models;
using ProductManager.WebApp.Models;

namespace ProductManager.WebApp.Mappings
{
    internal class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<ProductCategoryModel, ProductCategoryViewModel>();
            CreateMap<ProductSubCategoryModel, ProductSubCategoryViewModel>();
        }

        public override string ProfileName
        {
            get { return "ModelToViewModelMappingProfileMappings"; }
        }
    }
}