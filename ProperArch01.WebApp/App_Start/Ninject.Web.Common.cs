[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProperArch01.WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProperArch01.WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace ProperArch01.WebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    using ProperArch01.Domain.Services;
    using ProperArch01.Contracts.Services;
    using ProperArch01.Contracts.Queries;
    using ProperArch01.Contracts.Commands;
    using ProperArch01.Persistence.Commands;
    using ProperArch01.Persistence.Queries;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // services
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IClassTypeService>().To<ClassTypeService>();
            kernel.Bind<IClassTimetableService>().To<ClassTimetableService>();
            kernel.Bind<IColourServices>().To<ColourServices>();
            kernel.Bind<IHolidayService>().To<HolidayService>();
            kernel.Bind<IScheduledClassService>().To<ScheduledClassService>();

            // readers
            kernel.Bind<IGymUserReader>().To<GymUserReader>();
            kernel.Bind<IClassTypeReader>().To<ClassTypeReader>();
            kernel.Bind<IClassTimetableReader>().To<ClassTimetableReader>();
            kernel.Bind<IHolidayReader>().To<HolidayReader>();
            kernel.Bind<IScheduledClassReader>().To<ScheduledClassReader>();

            // writers
            kernel.Bind<IGymUserWriter>().To<GymUserWriter>();
            kernel.Bind<IClassTypeWriter>().To<ClassTypeWriter>();
            kernel.Bind<IClassTimetableWriter>().To<ClassTimetableWriter>();
            kernel.Bind<IHolidayWriter>().To<HolidayWriter>();
            kernel.Bind<IScheduledClassWriter>().To<ScheduledClassWriter>();
        }        
    }
}