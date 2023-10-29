using System.Text.Json;

namespace DebugApi.Features.Users;

public class JsonFileReader
{
    public static async Task<List<T>> ReadJsonFileAsync<T>(string filePath, CancellationToken cancellationToken = default)
    {
        string jsonContent = await File.ReadAllTextAsync(filePath, cancellationToken);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var data = JsonSerializer.Deserialize<List<T>>(jsonContent, options)!;
        return data;
    }
}

