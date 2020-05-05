using System.Diagnostics;
using System.Collections.Generic;

namespace System
{
    internal static class ThrowHelper
    {
        public static Exception GetSegmentCtorValidationFailedException<T>(T[] array, int offset, int count)
        {
            if (array == null)
                return new ArgumentNullException(nameof(array));

            if (offset < 0)
                return new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");

            if (count < 0)
                return new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");

            return new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
        }

        public static Exception GetSegmentCtorValidationFailedException<T>(IReadOnlyList<T> list, int offset, int count)
        {
            if (list == null)
                return new ArgumentNullException(nameof(list));

            if (offset < 0)
                return new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");

            if (count < 0)
                return new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");

            return new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
        }

        public static Exception GetSegmentCtorValidationFailedException<T>(IList<T> list, int offset, int count)
        {
            if (list == null)
                return new ArgumentNullException(nameof(list));

            if (offset < 0)
                return new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");

            if (count < 0)
                return new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");

            return new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
        }

        public static Exception GetSegmentCtorValidationFailedException<T>(ISegmentSource<T> source, int offset, int count)
        {
            if (source == null)
                return new ArgumentNullException(nameof(source));

            if (offset < 0)
                return new ArgumentOutOfRangeException(nameof(offset), "Non-negative number required.");

            if (count < 0)
                return new ArgumentOutOfRangeException(nameof(count), "Non-negative number required.");

            return new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
        }

        public static Exception GetArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
        {
            return new ArgumentOutOfRangeException(GetArgumentName(argument), GetResourceString(resource));
        }

        public static Exception GetArgumentOutOfRange_IndexException()
        {
            return GetArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_Index);
        }

        public static Exception GetArgumentOutOfRange_CountException()
        {
            return GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Index);
        }

        public static Exception GetInvalidOperationException_InvalidOperation_EnumNotStarted()
        {
            return new InvalidOperationException("Enumeration has not started. Call MoveNext.");
        }

        public static Exception GetInvalidOperationException_InvalidOperation_EnumEnded()
        {
            return new InvalidOperationException("Enumeration already finished.");
        }

        private static string GetArgumentName(ExceptionArgument argument)
        {
            switch (argument)
            {
                case ExceptionArgument.index:
                    return "index";

                case ExceptionArgument.count:
                    return "count";

                default:
                    Debug.Fail("The enum value is not defined, please check the ExceptionArgument Enum.");
                    return "";
            }
        }

        private static string GetResourceString(ExceptionResource resource)
        {
            switch (resource)
            {
                case ExceptionResource.ArgumentOutOfRange_Index:
                    return "Index was out of range. Must be non-negative and less than the size of the collection.";

                default:
                    Debug.Fail("The enum value is not defined, please check the ExceptionResource Enum.");
                    return "";
            }
        }
    }

    internal enum ExceptionArgument
    {
        index,
        count
    }

    internal enum ExceptionResource
    {
        ArgumentOutOfRange_Index
    }
}