namespace Fuzzy.Content;

public interface IContentProvider : IDisposable
{
    public Task LoadContentAsync(CancellationToken cancellationToken);
    
    public IEnumerable<string> GetPathParts();
}