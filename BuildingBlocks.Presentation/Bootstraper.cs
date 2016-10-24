using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using BuildingBlocks.Presentation.ViewModels;
using Caliburn.Micro;
using IContainer = Autofac.IContainer;

namespace BuildingBlocks.Presentation
{
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            ConfigureTypeMapping();
            BuildContainer();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var baseAssemblies = new List<Assembly>(base.SelectAssemblies());
            var thisAssembly = Assembly.GetAssembly(typeof(Bootstrapper));
            if (!baseAssemblies.Contains(thisAssembly))
            {
                baseAssemblies.Add(thisAssembly);
            }

            foreach (var assembly in baseAssemblies.ToList().Where(newAssembly => AssemblySource.Instance.Contains(newAssembly)))
            {
                baseAssemblies.Remove(assembly);
            }


            return baseAssemblies;
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key) ? _container.Resolve(service) : _container.ResolveNamed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        private static void ConfigureTypeMapping()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "BuildingBlocks.Presentation.Views",
                DefaultSubNamespaceForViewModels = "BuildingBlocks.Presentation.ViewModels"
            };
            ViewLocator.ConfigureTypeMappings(config);
            ViewModelLocator.ConfigureTypeMappings(config);
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.Name.EndsWith("View"))
                .AsSelf()
                .InstancePerDependency();

            builder.Register<IWindowManager>(c => new WindowManager())
                .InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator())
                .SingleInstance();

            _container = builder.Build();
        }
    }
}
