using RectangleApi.Domain.Models;

namespace RectangleApi.Domain.Services;

public interface IRectangleService
{
    Task<Rectangle?> GetRectangle();
    
    Task<Rectangle?> UpdateRectangle(Rectangle rectangle);
}