using Client;
using Client.Server;
using Client.Settings;
using CommunityToolkit.Mvvm.Messaging;
using Ninject.Modules;
using System.Drawing;
using System.Net;
using TestLib.Abstractions;
using TestLib.Classes.Network;

namespace Server.Ninject
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ISerializer>().To<JsonSerializer>();
            Bind<IMessenger>().ToMethod((context) => WeakReferenceMessenger.Default);
            Bind<ApplicationViewModel>().ToMethod((e)=> ApplicationViewModel.Instance);
            Bind<IServerService>().To<TcpServerService>()
                .WithConstructorArgument<ISerializer>(DI.Create<ISerializer>())
                .WithConstructorArgument<IMessenger>(DI.Create<IMessenger>());
            Bind<IFtpProvider>().To<WinScpFtpWorker>().WithConstructorArgument<string>(Settings.FtpHostAddress);
        }
    }
}
