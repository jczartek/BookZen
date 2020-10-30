using RepositoryLayer;
using System;
using Unity;

namespace ServiceLayer
{
    public static class ServiceFactory
    {
        public static IBookService CreateBookService()
        {
            return IoC.Instance.Container.Resolve<IBookService>();
        }

        public static void CreateBookService(Action<IBookService> serve)
        {
            using (var service = CreateBookService())
            {
                serve(service);
            }
        }

        public static void CreateBookService<TArg>(Action<IBookService, TArg> serve, TArg arg)
        {
            using (var servcie = CreateBookService())
            {
                serve(servcie, arg);
            }
        }

        public static TResult CreateBookService<TResult>(Func<IBookService, TResult> serve)
        {
            using (var service = CreateBookService())
            {
                return serve(service);
            }
        }

        public static TResult CreateBookService<TResult, TArg>(Func<IBookService, TArg, TResult> serve, TArg arg)
        {
            using (var service = CreateBookService())
            {
                return serve(service, arg);
            }
        }
    }
}
