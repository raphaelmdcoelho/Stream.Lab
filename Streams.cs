namespace Streams.Lab.Streams;

public class Streams
{
    public async Task<Stream> GetFileStream(string path)
    {
        return await Task.FromResult(File.Open(path, FileMode.Open));
    }

    public async Task<string> GetStringContent(Stream stream)
    {
        using StreamReader sr = new StreamReader(stream);
        return await sr.ReadToEndAsync();
    }
}