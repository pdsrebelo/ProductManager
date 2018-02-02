using AutoMapper;
using ProductManager.WebApi.Models;
using ProductManager.WebApp.Models;

namespace ProductManager.WebApp.Mappings
{
    internal class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
        {
            CreateMap<ProductViewModel, ProductModel>();
            CreateMap<ProductCategoryViewModel, ProductCategoryModel>();
            CreateMap<ProductSubCategoryViewModel, ProductSubCategoryModel>();
        }

        public override string ProfileName
        {
            get { return "ViewModelToModelMappingProfileMappings"; }
        }
    }
}