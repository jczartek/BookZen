using AutoMapper;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public static class Mapping
    {
        private static  MapperConfiguration mapperConfiguration = null;
        private static  Mapper mapper = null;
        public static void Init()
        {
            Configure();
        }

        private static void Configure()
        {
            if (mapperConfiguration != null) return;

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<Book, BookDto>()
                .ForMember(d => d.Authors, ops => ops.MapFrom(s => string.Join(",", s.AuthorsLink.Select(a => a.Author.Name))))
                .ForMember(d => d.IsOnLoan, ops => ops.MapFrom(s => s.BookRental != null))
                .ForMember(d => d.NameOfBorrower, ops => ops.MapFrom(s => s.BookRental != null ? s.BookRental.Name : default))
                .ForMember(d => d.DateBorrowing, ops => ops.MapFrom(s => s.BookRental != null ? s.BookRental.DateBorrowing : default));
            });
        }

        public static Mapper Mapper()
        {
            if (mapperConfiguration == null) throw new NullReferenceException("the config of the mapper can't be null. Return Mapping.Init() to initialize mapper.");

            return mapper ??= new Mapper(mapperConfiguration);

        }
    }
}
