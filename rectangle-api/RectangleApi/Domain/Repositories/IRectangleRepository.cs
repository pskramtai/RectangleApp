using RectangleApi.Domain.Models;

namespace RectangleApi.Domain.Repositories;

public interface IRectangleRepository
{
    Task<Rectangle?> GetRectangle();
    
    Task<Rectangle?> UpdateRectangle(Rectangle rectangle);
}