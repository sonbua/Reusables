using System;
using System.Linq;

namespace Reusables.Util.Extensions
{
    public static class DependencyResolutionExtensions
    {
        public static TDecoratee DecoratedWith<TDecorator, TDecoratee>(this TDecoratee decoratee) where TDecorator : TDecoratee
        {
            return decoratee.DecoratedWith(typeof(TDecorator));
        }

        public static TDecoratee DecoratedWith<TDecoratee>(this TDecoratee decoratee, Type decoratorType)
        {
            if (!typeof(TDecoratee).IsAssignableFrom(decoratorType))
                throw new ArgumentException($"{decoratorType} is not assignment compatible with {typeof(TDecoratee)}");

            var decoratorConstructors = decoratorType.GetConstructors();

            if (decoratorConstructors.Length != 1)
                throw new ArgumentException($"{decoratorType} should have a single public constructor, but it has {decoratorConstructors.Length}.");

            var decoratorConstructor = decoratorConstructors[0];

            if (!decoratorConstructor.IsPublic)
                throw new ArgumentException($"{decoratorType} should have a single public constructor, but this constructor isn't.");

            var decoratorConstructorParameterInfos = decoratorConstructor.GetParameters();

            if (decoratorConstructorParameterInfos.Length == 0)
                throw new ArgumentException($"{decoratorType}'s constructor should have at least one parameter.");

            var decorateeType = typeof(TDecoratee);
            var decorateeParameterInDecoratorConstructorCount = decoratorConstructorParameterInfos.Count(x => x.ParameterType.IsAssignableFrom(decorateeType));

            if (decorateeParameterInDecoratorConstructorCount != 1)
                throw new ArgumentException($"{decoratorType}'s constructor should have a single parameter of '{decorateeType}' type, but it has {decorateeParameterInDecoratorConstructorCount}.");

            var constructorParameters = decoratorConstructorParameterInfos.Select(x =>
                                                                                  {
                                                                                      var parameterType = x.ParameterType;
                                                                                      return parameterType.IsAssignableFrom(decorateeType)
                                                                                                 ? decoratee
                                                                                                 : DefaultServiceProvider.Current.GetService(parameterType);
                                                                                  }).ToArray();

            return (TDecoratee) decoratorConstructor.Invoke(constructorParameters);
        }
    }
}
