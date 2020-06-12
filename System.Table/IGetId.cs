namespace System.Table
{
    public interface IGetId<T> where T : IEntry
    {
        int GetId(T entry);
    }
}
