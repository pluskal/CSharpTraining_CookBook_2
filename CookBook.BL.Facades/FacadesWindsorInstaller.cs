using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CookBook.BL.Facades.Mappings;
using CookBook.BL.Repository;
using CookBook.DAL;

namespace CookBook.BL.Facades
{
    public class FacadesWindsorInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new RepositoryWindsorInstaller());

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IngredientDTOMappingProfile>();
                cfg.AddProfile<RecipeDTOMappingProfile>();
            });
            container.Register(Component.For<IMapper, Mapper>().Instance(new Mapper(configurationProvider)));

            container.Register(Component.For<IngredientFacade>());
            container.Register(Component.For<RecipeFacade>());
        }
    }
}