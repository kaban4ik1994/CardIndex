using CardIndex.DBInteractions.Concrete;
using CardIndex.DBInteractions.Interface;
using CardIndex.Repositories.Concrete;
using CardIndex.Repositories.Interface;
using CardIndex.Services.Concrete;
using CardIndex.Services.Interface;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace CardIndex.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            //Db interactions
            container.RegisterType<IDbFactory, DbFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            //Repositories
            container.RegisterType<IGenreRepository, GenreRepository>();
            container.RegisterType<IAuthorRepository, AuthorRepository>();
            container.RegisterType<IBookRepository, BookRepository>();

            //Services
            container.RegisterType<IBookService, BookService>();
            container.RegisterType<IAuthorService, AuthorService>();
            container.RegisterType<IGenreService, GenreService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}