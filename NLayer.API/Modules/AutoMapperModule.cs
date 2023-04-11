using Autofac;
using AutoMapper;
using NLayer.Service.Mapping;
using Module = Autofac.Module;
namespace NLayer.API.Modules
{
    public class AutoMapperModule: Module
    {
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
