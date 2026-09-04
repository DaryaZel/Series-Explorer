using System;
using System.Web.Http;
using Ninject;
using Series.Api.Data;
using Series.Api.Infrastructure;
using Series.Api.Services;

namespace Series.Api
{
    public static class NinjectConfig
    {
        public static void Register(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            var kernel = CreateKernel();

            config.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<SeriesDbContext>().ToSelf();
            kernel.Bind<ISeriesService>().To<SeriesService>();
            kernel.Bind<IObservationsService>().To<ObservationsService>();

            return kernel;
        }
    }
}
