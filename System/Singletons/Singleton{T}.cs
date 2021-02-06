namespace System
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;

        private static readonly object Lock = new object();

        public static T Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new T());
                }
            }
        }
    }
}
