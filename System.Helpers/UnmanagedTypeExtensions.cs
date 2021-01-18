using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Helpers
{
    public static class UnmanagedTypeExtensions
    {
        private static readonly Dictionary<Type, bool> _cache = new Dictionary<Type, bool>();

        public static bool IsUnmanaged(this Type t)
        {
            var result = false;

            if (_cache.ContainsKey(t))
                return _cache[t];

            if (t.IsPrimitive || t.IsPointer || t.IsEnum)
                result = true;
            else
                if (t.IsValueType && t.IsGenericType)
            {
                var areGenericTypesAllBlittable = t.GenericTypeArguments.All(x => IsUnmanaged(x));
                if (areGenericTypesAllBlittable)
                    result = t.GetFields(BindingFlags.Public |
                                         BindingFlags.NonPublic | BindingFlags.Instance)
                              .All(x => IsUnmanaged(x.FieldType));
                else
                    return false;
            }
            else
                if (t.IsValueType)
                result = t.GetFields(BindingFlags.Public |
                                     BindingFlags.NonPublic | BindingFlags.Instance)
                          .All(x => IsUnmanaged(x.FieldType));

            _cache.Add(t, result);
            return result;
        }
    }
}