namespace Infrastructure.Utilities
{
    public static class PrimitiveExtensions
    {
        public static string NullIfEmpty(this string text)
        {
            return text?.Length == 0 ? null : text;
        }
    }
}