public static class StreamReaderExtensions {
    public static IEnumerable<string> ToIEnumerable(this StreamReader stream) {
        if (stream == null) {
            throw new ArgumentNullException(nameof(stream));
        }

        while (true) {
            string? line = stream.ReadLine();
            if (line == null) {
                yield break;
            }
            yield return line;
        }
    }
}