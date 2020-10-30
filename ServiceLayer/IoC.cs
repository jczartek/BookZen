using DataLayer;
using Unity;
using Unity.Injection;

namespace ServiceLayer
{
    public class IoC
    {
        private static IoC _instance;
        private static readonly object _lock = new object();
        public static IoC Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new IoC();
                    }
                }
                return _instance;
            }
        }

        public IUnityContainer Container { get; } = new UnityContainer();

        private void Init()
        {
            Container.RegisterFactory<DbCoreContext>(_ => DbCoreContextFactory.Create());
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new InjectionConstructor(Container.Resolve<DbCoreContext>()));
            Container.RegisterType<IBookService, BookService>(new InjectionConstructor(Container.Resolve<IUnitOfWork>()));
        }

        private IoC()
        {
            Init();
        }
    }
}
