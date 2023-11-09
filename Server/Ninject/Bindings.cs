using Ninject.Modules;
using Repository;
using Server.Infrastructure;
using TestLib.Abstractions;

namespace Server.Ninject
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IGenericUnitOfWork>().To<GenericUnitOfWork>();
            Bind<IFileExplorerProvider>().To<FileExplorer>();
        }
    }
}
