using CardIndex.Data.DBInteractions.Concrete;
using CardIndex.Data.DBInteractions.Interface;
using CardIndex.Data.Repositories.Concrete;
using CardIndex.Data.Repositories.Interface;
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
            container.RegisterType<IDbFactory, DbFactory>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());

            //Repositories
            container.RegisterType<IGenreRepository, GenreRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IAuthorRepository, AuthorRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IBookRepository, BookRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IBookAuthorRepository, BookAuthorRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IBookGenreRepository, BookGenreRepository>(new PerResolveLifetimeManager());

            //Services
            container.RegisterType<IBookService, BookService>(new PerResolveLifetimeManager());
            container.RegisterType<IAuthorService, AuthorService>(new PerResolveLifetimeManager());
            container.RegisterType<IGenreService, GenreService>(new PerResolveLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}