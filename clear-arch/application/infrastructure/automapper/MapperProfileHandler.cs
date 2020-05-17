﻿using application.interfaces.mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace application.infrastructure.automapper
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }

    public static class MapperProfileHandler
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (from type in types
                            from instance in type.GetInterfaces()
                            where
                                instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                !type.IsAbstract &&
                                !type.IsInterface
                            select new Map
                            {
                                Source = type.GetInterfaces().First().GetGenericArguments().First(),
                                Destination = type
                            }).ToList();

            return mapsFrom;
        }

        public static IList<IHaveCustomMapping> LoadCustomMapping(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = (from type in types
                            from instance in type.GetInterfaces()
                            where
                                  typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                                  !type.IsAbstract &&
                                  !type.IsInterface
                            select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}