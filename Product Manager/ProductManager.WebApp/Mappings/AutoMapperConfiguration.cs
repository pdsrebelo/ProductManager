using AutoMapper;

namespace ProductManager.WebApp.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToModelMappingProfile>();
                x.AddProfile<ModelToViewModelMappingProfile>();
            });
        }
    }
}