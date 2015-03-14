// Partial Copyright (c) LogoUI Software Solutions LTD
// Author: Gennady Verdel
// This source file is the part of LogoFX Framework http://logofx.codeplex.com
// See accompanying licences and credits.

using System.Collections.Generic;
using System.Linq;

// ReSharper disable CheckNamespace
namespace System.Reflection
// ReSharper restore CheckNamespace
{

    /// <summary>
    /// Provides extension methods applicable to all objects.
    /// </summary>
    public static class ReflectionExtensions
    {
        public static bool HasInterface(this object o, Type interfaceType)
        {
            return o.GetType()
#if !WinRT
                .GetInterface(interfaceType.Name,true) != null;

#else
.GetTypeInfo().ImplementedInterfaces.Any(t => t.Name.Equals(interfaceType.Name, StringComparison.OrdinalIgnoreCase));
#endif


        }

        public static void CallMethod(this object o, string methodName, object[] args)
        {
            CallMethod<object>(o, methodName, args);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return type.
#if !WinRT
                GetProperties()
#else
GetTypeInfo().DeclaredProperties
#endif
;
        }

        public static MethodInfo GetSetMethod(this PropertyInfo property, bool nonPublic)
        {
#if WinRT
            var method = property.SetMethod;
            return method == null || (nonPublic && !method.IsPublic) || (!nonPublic && method.IsPublic) ? method : null;
#else
            return property.GetSetMethod(nonPublic);
#endif
        }


        /// <summary>
        /// Calls the <see cref="object"/> instance method by its name.
        /// </summary>
        /// <typeparam name="T">Method return value type.</typeparam>
        /// <param name="o">The <see cref="object"/> instance.</param>
        /// <param name="methodName">Method name.</param>
        /// <param name="args">Method arguments.</param>
        /// <returns>Result of call the <see cref="object"/> instance method by its name.</returns>
        public static T CallMethod<T>(this object o, string methodName, object[] args)
        {
            object obj = GetMethod(o, methodName, args).Invoke(o, args);
            return (T)obj;
        }

        public static IEnumerable<FieldInfo> GetFields(this Type type)
        {
            return type
            #if !WinRT
                .GetFields()
            #else
                .GetTypeInfo().DeclaredFields
            #endif
;
        }

        public static PropertyInfo GetProperty(this Type type, string propertyName
#if !WinRT
            ,BindingFlags attr
#endif
)
        {
            return
#if !WinRT
            type.GetProperty(propertyName,attr);
#else
 type.GetTypeInfo().DeclaredProperties.SingleOrDefault(t => t.Name == propertyName);
#endif
        }

        public static bool IsEnum(this Type type)
        {
            return type
#if WinRT
                .GetTypeInfo()
#endif
                .IsEnum;
        }

        public static bool IsValueType(this Type type)
        {
            return type
#if WinRT
                .GetTypeInfo()
#endif
                .IsValueType;
        }

        public static bool IsGenericType(this Type type)
        {
            return type
#if WinRT
                .GetTypeInfo()
#endif
                .IsGenericType;
        }

        public static bool IsSubclassOf(this Type type,Type other)
        {
            return type
#if WinRT
                .GetTypeInfo()
#endif
                .IsSubclassOf(other);

        }

        public static ConstructorInfo GetConstructor<TModel>(this Type type) where TModel : class
        {
            return type.
#if !WinRT
                GetConstructor(new[] { typeof(TModel) })
#else
GetTypeInfo().DeclaredConstructors.SingleOrDefault(t =>
{
    var p = t.GetParameters();
    return p.Length == 1 &&
           p.Single().ParameterType ==
           typeof(TModel);
})
#endif
;
        }       

        public static bool IsInstanceOfType(this Type type, object other)
        {
            return
#if !WinRT
                type.IsInstanceOfType(other);
#else
 other != null && type.GetTypeInfo().IsAssignableFrom(other.GetType().GetTypeInfo());
#endif
        }

        public static bool IsAssignableFrom(this Type type, Type other)
        {
            return
#if !WinRT
                type.IsAssignableFrom(other);
#else
 type.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
#endif
        }

        private static MethodInfo GetMethod(object o, string methodName, object[] args)
        {
            var type = o.GetType();
            var method = type.
#if !WinRT
                GetMethod(methodName);
                if (method == null)
                {
                    method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
                }
#else
GetTypeInfo().DeclaredMethods.FirstOrDefault(t => t.Name == methodName);
#endif
            return method;
        }

        public static IEnumerable<Attribute> GetCustomAttributes(this Type type, Type at, bool inherits)
        {
            return type.
#if WinRT
                GetTypeInfo().
#endif
                GetCustomAttributes(at, inherits)
#if !WinRT
                .Cast<Attribute>()
#endif
                ;
        }
    }
}
