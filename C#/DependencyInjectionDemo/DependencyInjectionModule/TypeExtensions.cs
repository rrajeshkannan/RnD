using System;
using System.Reflection;

namespace DependencyInjectionModule
{
    public static class TypeExtensions
    {
        public static bool IsDerivedFrom<TType>(this TypeInfo typeInfo)
        {
            var ruleType = typeof(TType).GetTypeInfo();
            return ruleType.IsAssignableFrom(typeInfo);
        }

        public static bool IsDerivedFrom<TType>(this Type type)
        {
            return IsDerivedFrom<TType>(type.GetTypeInfo());
        }

        public static bool IsConcreteType(this TypeInfo typeInfo)
        {
            return 
                !typeInfo.IsAbstract &&
                !typeInfo.IsInterface &&
                !typeInfo.IsGenericTypeDefinition;
        }

        public static bool IsConcreteType(this Type type)
        {
            return IsConcreteType(type.GetTypeInfo());
        }
    }
}