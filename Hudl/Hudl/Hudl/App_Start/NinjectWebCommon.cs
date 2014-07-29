using Hudl.Services.Calendar;
using Hudl.Services.Events;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Hudl.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Hudl.App_Start.NinjectWebCommon), "Stop")]

namespace Hudl.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using System;
    using System.Web;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(); // you'll add modules to the parameter list here
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<Services.IConnectionFactory>().To<Services.ConnectionFactory>();
                kernel.Bind<IEventsDataProvider>().To<EventsDataProvider>();
                kernel.Bind<IEventsBuilder>().To<EventsBuilder>();

                kernel.Bind<ICalendarDataProvider>().To<CalendarDataProvider>();
                kernel.Bind<ICalendarBuilder>().To<CalendarBuilder>();

                //RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        ///// <summary>
        ///// Load your modules or register your services here!
        ///// </summary>
        ///// <param name="kernel">The kernel.</param>
        //private static void RegisterServices(IKernel kernel)
        //{
        //}
    }
}