using System;
using System.Linq.Expressions;
using System.Reflection;
using Moq;

namespace JasonSoft.Extensions
{
    /// <summary>
    /// Extension method that provides duck typing for extended types.
    /// 
    /// Copyright (c) 2014 Jason C. Bowers
    /// http://www.linkedin.com/in/jcbowers
    /// https://github.com/jcbowers/DuckTyping
    /// 
    /// Released under The MIT License (MIT) 
    /// http://choosealicense.com/licenses/mit/
    /// </summary>
    public static class DuckEntensions
    {
        /// <summary>
        /// Returns a proxy object of type T who's implementation is provided by the subject of the duck type cast
        /// </summary>
        /// <typeparam name="T">The generic type whose interface the Duck Type will implement</typeparam>
        /// <param name="subject">The object to be casted</param>
        /// <returns>A proxy object of type T which wraps the subject object instance</returns>
        /// <exception cref="InvalidCastException">Throws if subject type does not have the same signature as the target type</exception>
        public static T DuckCast<T>(this object subject) where T : class
        {
            Type targetType = typeof(T);
            var proxy = new Mock<T>();
            var subjectType = subject.GetType();
            var targetExpression = Expression.Parameter(targetType);

            foreach (var propertyInfo in targetType.GetProperties())
            {
                ProxyProperty<T>(subject, targetType, proxy, subjectType, targetExpression, propertyInfo);
            }

            return proxy.Object;
        }

        private static void ProxyProperty<T>(object subject, Type targetType, Mock<T> proxy, Type subjectType, ParameterExpression targetExpression, PropertyInfo propertyInfo) where T : class
        {
            if (subjectType.GetProperty(propertyInfo.Name) == null)
            {
                throw new InvalidCastException(string.Format("Property '{0}' not found on subject of the cast.", propertyInfo.Name));
            }

            var property = Expression.Property(targetExpression, propertyInfo.Name);
            var funcType = typeof(Func<,>).MakeGenericType(targetType, propertyInfo.PropertyType);
            var lambda = Expression.Lambda(funcType, property, targetExpression);

            var setupMethodInfo = proxy.GetType().GetMethod("SetupGet");
            var genericMethod = setupMethodInfo.MakeGenericMethod(propertyInfo.PropertyType);
            var getter = genericMethod.Invoke(proxy, new object[] { lambda });

            var returnsMethodInfo = getter.GetType().GetMethod("Returns", new Type[] { propertyInfo.PropertyType });
            returnsMethodInfo.Invoke(getter, new object[] { subjectType.GetProperty(propertyInfo.Name).GetValue(subject) });
        }
    }
}
