
namespace PonyText.Common.Renderer
{
    public interface IPropertyCollection
    {
        bool TryGetProperty<T>(string key, out T value);
    }
}