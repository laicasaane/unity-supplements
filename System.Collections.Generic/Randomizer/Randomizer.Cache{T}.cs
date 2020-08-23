namespace System.Collections.Generic
{
    public static partial class Randomizer
    {
        public interface ICache<T>
        {
            List<T> Input { get; }

            List<T> Output { get; }

            void Clear();
        }

        private sealed class DefaultCache<T> : ICache<T>
        {
            public List<T> Input { get; } = new List<T>();

            public List<T> Output { get; } = new List<T>();

            public void Clear()
            {
                this.Input.Clear();
                this.Output.Clear();
            }

            public static DefaultCache<T> Default { get; } = new DefaultCache<T>();
        }
    }
}