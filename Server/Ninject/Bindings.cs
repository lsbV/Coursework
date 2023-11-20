using CommunityToolkit.Mvvm.Messaging;
using DALTestsDB;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using Repository;
using Server.Infrastructure;
using TestLib.Abstractions;
using TestLib.Classes.Logger;
using TestLib.Classes.Network;
using Xceed.Wpf.Toolkit;

namespace Server.Ninject
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            //Bind<IGenericUnitOfWork>().To<GenericUnitOfWork>().WithConstructorArgument<DbContext>(new SampleMyDbContextFactory().CreateDbContext(null!));
            Bind<IGenericUnitOfWork>().ToConstructor(c => new GenericUnitOfWork(new SampleMyDbContextFactory().CreateDbContext(null!)));
            Bind<IFileExplorerProvider>().To<FileExplorer>();
            Bind<IEncryptor>().To<Sha256Encryptor>();
            Bind<ISerializer>().To<JsonSerializer>();
            Bind<ILogger>().To<MethodLogger>();
            Bind <IMessenger>().ToMethod((context) => WeakReferenceMessenger.Default);

        }
    }
}
