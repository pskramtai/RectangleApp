using System.Text.Json;
using RectangleApi.Domain.Models;
using RectangleApi.Domain.Repositories;

namespace RectangleApi.Infrastructure.Repositories;

public class RectangleFileRepository(ILogger<RectangleFileRepository> logger, string filePath) 
    : IRectangleRepository
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public async Task<Rectangle?> GetRectangle()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        
        var contents = await File.ReadAllTextAsync(filePath);
        
        try
        {
            return JsonSerializer.Deserialize<Rectangle>(contents, SerializerOptions);
        }
        catch
        {
            logger.LogError("Unable to parse file contents.");
            throw;
        }
    }

    public async Task<Rectangle?> UpdateRectangle(Rectangle rectangle)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }
        
        var json = JsonSerializer.Serialize(rectangle, SerializerOptions);
        
        await File.WriteAllTextAsync(filePath, json);

        return rectangle;
    }
}