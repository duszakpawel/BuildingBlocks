using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using BuildingBlocks.BusinessLogic.Algorithm;
using BuildingBlocks.BusinessLogic.Interfaces;
using BuildingBlocks.BusinessLogic.Parsing;
using BuildingBlocks.BusinessLogic.Serialization;
using BuildingBlocks.Presentation.Common;
using BuildingBlocks.Presentation.ViewModels;
using Caliburn.Micro;
using IContainer = Autofac.IContainer;

namespace BuildingBlocks.Presentation
{
    /// <summary>
    ///     Autofac class configuration
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer _container;

        /// <summary>
        ///     Constructor of class
        /// </summary>
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
            foreach (
                var assembly in
                    baseAssemblies.ToList().Where(newAssembly => AssemblySource.Instance.Contains(newAssembly)))
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
            builder.Register<IBlockLogicProvider>(c => new BlockLogicProvider())
                .InstancePerLifetimeScope();
            builder.Register<IEvaluateFunctionProvider>(c => new EvaluateFunctionProvider())
                .InstancePerLifetimeScope();
            builder.Register<IBlocksParser>(c => new BlocksParser())
                .InstancePerDependency();
            builder.Register<ICustomDialogManager>(c => new CustomDialogManager())
                .SingleInstance();
            builder.Register<IComputationsSerializer>(c => new ComputationsSerializer())
                .SingleInstance();
            builder.Register<IBlocksPreprocessor>(c => new BlocksPreprocessor())
            .SingleInstance();

            _container = builder.Build();
        }
    }
}