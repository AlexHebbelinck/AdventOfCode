namespace Common.Models
{
    public sealed class FileInputCollection : List<string>
    {
        public FileInputCollection TrimTrailingNewLine()
            => [
                    .. this.Select(x => x.Trim('\r')).Where(x => !string.IsNullOrEmpty(x)).ToList()
                ];
    }
}