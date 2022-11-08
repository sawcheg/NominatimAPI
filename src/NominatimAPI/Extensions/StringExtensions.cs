namespace NominatimAPI.Extensions {
    internal static class StringExtensions {
        internal static bool HasValue(this string str) {
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}