// Source: https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/TypeCache.cs

namespace System.Helpers
{
    public class TypeCache<T>
    {
        public static readonly Type Type = typeof(T);

        public static readonly bool IsUnmanaged = Type.IsUnmanaged();
    }
}