using Ninject;

namespace Server.Ninject
{
    public static class  DI
    {
        static StandardKernel kernel;
        static DI()
        {
            kernel = new StandardKernel();
            kernel.Load(new Bindings());
        }
        public static T Create<T>()
        {
            return kernel.Get<T>();
        }
    }
}
