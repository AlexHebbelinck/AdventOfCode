namespace Common.Extensions
{
    public static class ListExtensions
    {
        public static T AddWithReturn<T>(this List<T> list, T obj)
        {
            list.Add(obj);
            return obj;
        }
    }
}