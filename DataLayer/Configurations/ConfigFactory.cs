using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DataLayer.Configurations
{
    public static class ConfigFactory
    {
        public static IEntityTypeConfiguration<TEntity> ConfigFor<TEntity>()
            where TEntity : class, new()
        {
            var nameTypeEntity = typeof(TEntity).Name;
            var nameTypeContigClass = $"DataLayer.Configurations.{nameTypeEntity}Config";

            if (!TryFindType(nameTypeContigClass, out Type type))
                throw new ArgumentException($"{nameTypeContigClass} doesn't exist");

            if (!type.Implements<IEntityTypeConfiguration<TEntity>>())
                throw new ArgumentException($"{ typeof(IEntityTypeConfiguration<TEntity>).Name} is not implemented by {nameTypeContigClass}");

            return (IEntityTypeConfiguration<TEntity>) type.Assembly.CreateInstance(nameTypeContigClass);
        }

        private static bool Implements<I>(this Type source) where I : class
        {
            return typeof(I).IsInterface && typeof(I).IsAssignableFrom(source);
        }

        private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>();
        public static bool TryFindType(string typeName, out Type t)
        {
            lock (typeCache)
            {
                if (!typeCache.TryGetValue(typeName, out t))
                {
                    foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        t = a.GetType(typeName);
                        if (t != null)
                            break;
                    }
                    typeCache[typeName] = t;
                }
            }
            return t != null;
        }
    }
}
