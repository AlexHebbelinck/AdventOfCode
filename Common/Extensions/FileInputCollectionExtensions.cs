using Common.Models;

namespace Common.Extensions
{
    public static class FileInputCollectionExtensions
    {
        public static T[][] ToJaggedArray<T>(this FileInputCollection inputs)
            => inputs.Select(x => x.Select(c => (T)Convert.ChangeType(c.ToString(), typeof(T))).ToArray()).ToArray();
    }
}