using RepositoryLayer;
using ServiceLayer.Abstract;
using ServiceLayer.Concrete;
using System;

namespace ServiceLayer
{
    public static class ServiceFactory
    {
        public static  IBookService CreateBookService()
        {
            return new BookService(RepositoryFactory.CreateBookRepository());
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

        public static TResult CreateBookService<TResult>(Func<IBookService,TResult> serve)
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

        public static IBookRentalService CreateBookRentalService()
        {
            return new BookRentalService(RepositoryFactory.CreateBookRentalRepository());
        }

        public static void CreateBookRentalService(Action<IBookRentalService> serve)
        {
            using (var service = CreateBookRentalService())
            {
                serve(service);
            }
        }

        public static void CreateBookRentalService<TArg>(Action<IBookRentalService, TArg> serve, TArg arg)
        {
            using (var service = CreateBookRentalService())
            {
                serve(service, arg);
            }
        }
        
        public static TResult CreateBookRentalService<TResult>(Func<IBookRentalService, TResult> serve)
        {
            using (var service = CreateBookRentalService())
            {
                return serve(service);
            }
        }

        public static TResult CreateBookRentalService<TResult, TArg>(Func<IBookRentalService, TArg, TResult> serve, TArg arg)
        {
            using (var service = CreateBookRentalService())
            {
                return serve(service, arg);
            }
        }
    }
}
