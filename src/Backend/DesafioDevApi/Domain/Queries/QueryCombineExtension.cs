using Serilog;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace DesafioDevApi.Domain.Queries
{
    public static class QueryCombineExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo do objeto para junção da expression</typeparam>
        /// <param name="filter1">Expression existente</param>
        /// <param name="filter2">Expression a ser unida</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Combine<T>(this Expression<Func<T, bool>> filter1, Expression<Func<T, bool>> filter2)
        {
            var rewrittenBody1 = new ReplaceVisitor(
                filter1.Parameters[0], filter2.Parameters[0]).Visit(filter1.Body);
            var newFilter = Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(rewrittenBody1, filter2.Body), filter2.Parameters);
            return newFilter;
        }
        /// <summary>
        /// Obter dados da propriedade com retorno em uma lista
        /// </summary>
        /// <param name="obj">objeto a ser retornado</param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertyDictionary(this object obj)
        {
            var propDictionary = new Dictionary<string, object>();

            var passedType = obj.GetType();

            foreach (var propertyInfo in passedType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(p => p.CanRead && p.CanWrite)?.ToArray())
            {
                var value = propertyInfo.GetValue(obj, null);
                if (value != null)
                    propDictionary.Add(propertyInfo.Name, value);
            }

            return propDictionary;
        }
        /// <summary>
        /// Obter o valor default do objeto
        /// </summary>
        /// <param name="type">Objeto para retorno do tipo padrão</param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            // If no Type was supplied, if the Type was a reference type, or if the Type was a System.Void, return null
            if (type == null || !type.IsValueType || type == typeof(void))
                return null;

            // If the supplied Type has generic parameters, its default value cannot be determined
            if (type.ContainsGenericParameters)
                throw new ArgumentException(
                    "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                    "> contains generic parameters, so the default value cannot be retrieved");

            // If the Type is a primitive type, or if it is another publicly-visible value type (i.e. struct), return a 
            //  default instance of the value type
            if (type.IsPrimitive || !type.IsNotPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe Activator.CreateInstance method could not " +
                        "create a default instance of the supplied value type <" + type +
                        "> (Inner Exception message: \"" + e.Message + "\")", e);
                }
            }

            // Fail with exception
            throw new ArgumentException("{" + MethodInfo.GetCurrentMethod() + "} Error:\n\nThe supplied value type <" + type +
                "> is not a publicly-visible type, so the default value cannot be retrieved");
        }
        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsPrimitive ||
                new Type[] {
            typeof(string),
            typeof(decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
                }.Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].IsSimpleType()
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="obj">Objeto a ser atualizado</param>
        /// <param name="objUpdate">Objeto a ser comparado</param>
        /// <returns></returns>
        public static T SetPropertyAutomap<T>(this T obj, object objUpdate) where T : class
        {
            var instance = Activator.CreateInstance(obj.GetType());
            var propertiesIntance = instance.GetPropertyDictionary();
            var passedType = obj.GetType();
            var passedUpdateType = objUpdate.GetPropertyDictionary();
            foreach (var propertyInfo in passedType.GetProperties())
            {
                try
                {
                    var name = propertyInfo.Name?.ToString();
                    var prop = GetDefault(propertyInfo.GetType());
                    var properttype = propertyInfo.GetType();
                    var value = passedUpdateType.FirstOrDefault(x => x.Key == name).Value;

                    if (value == null && passedUpdateType.Any(x => !x.Value.GetType().IsSimpleType() && x.Key.Contains(name) && propertyInfo?.PropertyType.Name.ToUpper() == x.Value?.GetType().Name.ToUpper()))
                        value = passedUpdateType.FirstOrDefault(x => x.Key.StartsWith(name) && propertyInfo?.PropertyType.Name.ToUpper() == x.Value?.GetType().Name.ToUpper()).Value;

                    var defaultValue = propertiesIntance.FirstOrDefault(x => x.Key == propertyInfo.Name);

                    if ((value != null && value != defaultValue.Value) && (propertyInfo.CanWrite && propertyInfo.CanRead) &&
                        (propertyInfo?.PropertyType.Name.ToUpper() == value?.GetType().Name.ToUpper() ||
                        (propertyInfo?.PropertyType?.GenericTypeArguments.Length > 0 && propertyInfo?.PropertyType?.GenericTypeArguments.FirstOrDefault().FullName.ToUpper() == value?.GetType().FullName.ToUpper())
                        ))
                    {
                        propertyInfo.SetValue(obj, value);
                    }
                    else if ((value != null && value != defaultValue.Value) && !value.GetType().IsSimpleType() && propertyInfo.PropertyType.Name.ToUpper().Contains(value.GetType().Name.ToUpper()))
                    {
                        if (propertyInfo is IEnumerable)
                        {
                            IList list = (IList)Activator.CreateInstance(propertyInfo.PropertyType);
                            var enumerator = ((IEnumerable)value).GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                list.Add(enumerator.Current);
                            }
                            propertyInfo.SetValue(obj, list);
                        }
                        else
                            propertyInfo.SetValue(obj, value);
                    }
                }
                catch (Exception erro)
                {
                    Log.Error(erro.Message, erro);
                }

            }
            return obj;
        }
    }
}
