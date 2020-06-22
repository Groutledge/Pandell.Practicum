using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Pandell.Practicum.App.Data;
using Pandell.Practicum.App.Domain;
using Pandell.Practicum.App.Repository;
using Pandell.Practicum.App.Services;

namespace Pandell.Practicum.App.Utility
{
    public static class Injector
    {
        private static readonly IWindsorContainer container;

        static Injector()
        {
            container = new WindsorContainer();
            RegisterInjector();
        }

        public static void AddApplicationDbContext(ApplicationDbContext applicationDbContext)
        {
            if (IsAlreadyRegistered()) return;
            RegisterRepositoryComponents(applicationDbContext);
            RegisterServiceComponents();
        }

        public static TServiceToResolve Resolve<TServiceToResolve>()
        {
            return container.Resolve<TServiceToResolve>();
        }

        public static bool IsAlreadyRegistered()
        {
            return container.Kernel.HasComponent(typeof(IWindsorContainer))
                   && container.Kernel.HasComponent(typeof(IRepository<RandomSequence, Guid>))
                   && container.Kernel.HasComponent(typeof(IRandomSequenceService));
        }

        public static void ReleaseAllRegistrations()
        {
            container.Release(container);
            container.Dispose();
        }
        
        private static void RegisterInjector()
        {
            container.Register(
                Component.For<IWindsorContainer>()
                    .Instance(container));
        }

        private static void RegisterRepositoryComponents(ApplicationDbContext applicationDbContext)
        {
            AddSpecificRepositoryComponent(new RandomSequenceRepository(applicationDbContext));
        }

        private static void AddSpecificRepositoryComponent<TDomain>(IRepository<TDomain, Guid> repositoryToAdd) where TDomain : class
        {
            container.Register(
                Component.For<IRepository<TDomain, Guid>>()
                    .Instance(repositoryToAdd)
                    .LifestyleSingleton());
        }
        
        private static void RegisterServiceComponents()
        {
            container.Register(
                Component.For<IIdService>()
                    .ImplementedBy<IdService>()
                    .LifestyleSingleton());

            container.Register(
                Component.For<IRandomSequenceGeneratorService>()
                    .ImplementedBy<RandomSequenceGeneratorService>()
                    .LifestyleSingleton());
            
            container.Register(
                Component.For<IRandomSequenceService>()
                    .ImplementedBy<RandomSequenceService>()
                    .LifestyleSingleton());
        }
    }
}