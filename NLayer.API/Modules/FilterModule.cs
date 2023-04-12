using Autofac;
using NLayer.API.Filters;
using Module = Autofac.Module;
namespace NLayer.API.Modules
{
    public class FilterModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(NotFoundFilter<,>));

            base.Load(builder);
        }
    }
}
