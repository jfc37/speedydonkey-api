using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName)
        {
            return OrderBy(source, fieldName, false);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string fieldName)
        {
            return OrderBy(source, fieldName, true);
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string fieldName, bool isDescending)
        {
            if (source == null)
                throw new ArgumentNullException("source", "source is null.");

            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName is null or empty.", "fieldName");

            var parts = fieldName.Split(' ');
            var propertyName = "";
            var tType = typeof(T);

            if (parts.Length > 0 && parts[0] != "")
            {
                propertyName = parts[0];

                PropertyInfo prop = tType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop == null)
                {
                    throw new ArgumentException(string.Format("No property '{0}' on type '{1}'", propertyName, tType.Name));
                }

                var funcType = typeof(Func<,>)
                    .MakeGenericType(tType, prop.PropertyType);

                var lambdaBuilder = typeof(Expression)
                    .GetMethods()
                    .First(x => x.Name == "Lambda" && x.ContainsGenericParameters && x.GetParameters().Length == 2)
                    .MakeGenericMethod(funcType);

                var parameter = Expression.Parameter(tType);
                var propExpress = Expression.Property(parameter, prop);

                var sortLambda = lambdaBuilder
                    .Invoke(null, new object[] { propExpress, new ParameterExpression[] { parameter } });

                var sorter = typeof(Queryable)
                    .GetMethods()
                    .FirstOrDefault(x => x.Name == (isDescending ? "OrderByDescending" : "OrderBy") && x.GetParameters().Length == 2)
                    .MakeGenericMethod(new[] { tType, prop.PropertyType });

                return (IQueryable<T>)sorter
                    .Invoke(null, new object[] { source, sortLambda });
            }

            return source;
        }
    }
}
