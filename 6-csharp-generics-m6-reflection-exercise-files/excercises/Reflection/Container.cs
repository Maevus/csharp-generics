using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Container
    {
        Dictionary<Type, Type> map = new Dictionary<Type, Type>();

        public ContainerBuilder For<TSource>()
        {
            return For(typeof(TSource));
        }

        public ContainerBuilder For(Type sourceType)
        {
            return new ContainerBuilder(this, sourceType);
        }

        public TSource Resolve<TSource>()
        {
            return (TSource)Resolve(typeof(TSource));
        }

        public object Resolve(Type sourceType)
        {
            if (map.ContainsKey(sourceType))
            {
                var destinationType = map[sourceType];
                return CreateInstance(destinationType); //allowing constructor params
            }
            else if (sourceType.IsGenericType && map.ContainsKey(sourceType.GetGenericTypeDefinition()))
            {
                var destination = map[sourceType.GetGenericTypeDefinition()];
                var closedDestination = destination.MakeGenericType(sourceType.GenericTypeArguments);
                return CreateInstance(closedDestination);
                // allows you to contruct an instantiatable closedType from just types, allowing you to pass in unknown classes
            }
            else if (!sourceType.IsAbstract)
            {
                return CreateInstance(sourceType);
            }
            else
            {
                throw new InvalidOperationException(); // a custom ex woudl be better here/
            }
        }

        private object CreateInstance(Type destinationType)
        {
            var parameters = destinationType.GetConstructors()
                                   .OrderByDescending(c => c.GetParameters().Count())
                                   .First()
                                   .GetParameters()
                                   .Select(p => Resolve(p.ParameterType))
                                   .ToArray();

            // normal container would have a strategy for picking mst appropriate ctor and handle bunch of edge cases.
            return Activator.CreateInstance(destinationType, parameters);
        }

        public class ContainerBuilder
        {
            public ContainerBuilder(Container container, Type sourceType)
            {
                this.container = container;
                this.sourceType = sourceType;

            }

            public ContainerBuilder Use<TDestination>()
            {
                return Use(typeof(TDestination));
            }

            public ContainerBuilder Use(Type destinationType)
            {
                container.map.Add(sourceType, destinationType);
                return this;
            }

            Container container;
            Type sourceType;
        }
    }
}
