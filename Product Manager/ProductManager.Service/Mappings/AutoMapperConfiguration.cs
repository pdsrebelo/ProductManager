using AutoMapper;

namespace ProductManager.Service.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile<ModelToDataAcessMappingProfile>();
                c.AddProfile<DataAcessToModelMappingProfile>();
            });
        }

        public static void Reset()
        {
            Mapper.Reset();
        }
    }
}
