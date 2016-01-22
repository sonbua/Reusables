using System;
using System.Linq;

namespace Reusables.Util.Extensions
{
    public static class DependencyResolutionExtensions
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// This plays as a dependency resolver. It should be set only once, while the application is bootstrapping.
        /// </summary>
        public static IServiceProvider ServiceProvider
        {
            private get
            {
                if (_serviceProvider == null)
                {
                    throw new MemberAccessException($"'{nameof(_serviceProvider)}' has not been set.");
                }

                return _serviceProvider;
            }
            set
            {
                if (_serviceProvider != null)
                {
                    throw new MemberAccessException($"'{nameof(_serviceProvider)}' has already been set. This should be set only once.");
                }

                _serviceProvider = value;
            }
        }

        public static TDecoratee DecoratedWith<TDecorator, TDecoratee>(this TDecoratee decoratee) where TDecorator : TDecoratee
        {
            var decoratorType = typeof (TDecorator);
            var decoratorConstructors = decoratorType.GetConstructors();

            if (decoratorConstructors.Length != 1)
            {
                throw new ArgumentException($"{decoratorType} should have a single public constructor, but it has {decoratorConstructors.Length}.");
            }

            var decoratorConstructor = decoratorConstructors[0];

            if (!decoratorConstructor.IsPublic)
            {
                throw new ArgumentException($"{decoratorType} should have a single public constructor, but this constructor isn't.");
            }

            var decoratorConstructorParameterInfos = decoratorConstructor.GetParameters();

            if (decoratorConstructorParameterInfos.Length == 0)
            {
                throw new ArgumentException($"{decoratorType}'s constructor should have at least one parameter.");
            }

            var decorateeType = typeof (TDecoratee);
            var decorateeParameterInDecoratorConstructorCount = decoratorConstructorParameterInfos.Count(x => x.ParameterType.IsAssignableFrom(decorateeType));

            if (decorateeParameterInDecoratorConstructorCount != 1)
            {
                throw new ArgumentException($"{decoratorType}'s constructor should have a single parameter of '{decorateeType}' type, but it has {decorateeParameterInDecoratorConstructorCount}.");
            }

            var constructorParameters = decoratorConstructorParameterInfos.Select(x =>
                                                                                  {
                                                                                      var parameterType = x.ParameterType;
                                                                                      return parameterType.IsAssignableFrom(decorateeType)
                                                                                                 ? decoratee
                                                                                                 : ServiceProvider.GetService(parameterType);
                                                                                  }).ToArray();

            return (TDecoratee) decoratorConstructor.Invoke(constructorParameters);
        }
    }
}
