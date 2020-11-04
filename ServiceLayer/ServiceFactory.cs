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
            serve(CreateBookService());
        }

        public static void CreateBookService<TArg>(Action<IBookService, TArg> serve, TArg arg)
        {
            serve(CreateBookService(), arg);
        }

        public static TResult CreateBookService<TResult>(Func<IBookService, TResult> serve)
        {
            return serve(CreateBookService());
        }

        public static TResult CreateBookService<TResult, TArg>(Func<IBookService, TArg, TResult> serve, TArg arg)
        {
            return serve(CreateBookService(), arg);
        }
    }
}
