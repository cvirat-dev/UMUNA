using Umuna.Core.Communication.Contracts;
using Umuna.Core.Services.Serialization;
using Umuna.Core.SharedData;
using UMUNA.AppManagement;
using UMUNA.Assets._Scripts.Services;
using UMUNA.Configuration;
using UMUNA.Data;
using UMUNA.SavingSystem;
using VContainer;
using VContainer.Unity;

namespace UMUNA.Assets._Scripts
{
    public class AppLiftetimeScope : LifetimeScope
    {
        public static IObjectResolver ContainerInstance { get; private set; }

        protected override void Configure(IContainerBuilder builder)
        {
            // Load configuration once
            var config = new Configurator<AppConfiguration>().Data;
            builder.RegisterInstance(config);

            // Core data/services
            builder.Register<UmunaData>(Lifetime.Singleton);
            builder.Register<ExportNotes>(Lifetime.Singleton);
            builder.Register<BindSystem>(Lifetime.Singleton)
                .AsImplementedInterfaces().AsSelf();

            // SaveLoadSystem needs dependencies
            builder.Register<ISaveLoadSystem, SaveLoadSystem>(Lifetime.Singleton)
                .WithParameter(config.FileSystemConfiguration.SerializationFormat);

            // Serializer and TCP service
            builder.RegisterInstance(
                SerializerFactory.Create<MessageDto>(
                    config.FileSystemConfiguration.SerializationFormat.Value));

            builder.Register<ITcpClientService<MessageDto>, TcpClientService<MessageDto>>(Lifetime.Singleton);


            // THE APP MANAGER ITSELF
            builder.Register<AppManager>(Lifetime.Singleton);
        }

        protected override void Awake()
        {
            base.Awake();
            ContainerInstance = Container;
        }

        public void Start()
        {
            AppManager appManager = Container.Resolve<AppManager>();
            appManager.Initialize(); // you can make this async void or wrap it
        }
    }
}