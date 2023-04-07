using Autofac;
using AutoMapper;
using NLayer.Service.Mapping;

namespace NLayer.API.Modules
{
    public class AutoMapperModule:Module
    {
        //TODO: AutoMapper classları buraya al
        protected override void Load(ContainerBuilder builder)
        {

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
                cfg.AddProfile(new CategoryProfile());
                cfg.AddProfile(new ProductFeatureProfile());

            });

            builder.RegisterInstance<IMapper>(mappingConfig.CreateMapper());
            
            base.Load(builder);
        }
    }
}
