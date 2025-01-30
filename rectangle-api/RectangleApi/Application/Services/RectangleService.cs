using RectangleApi.Domain.Models;
using RectangleApi.Domain.Repositories;
using RectangleApi.Domain.Services;

namespace RectangleApi.Application.Services;

public class RectangleService(
    IRectangleValidationService validationService, 
    IRectangleRepository repository
) : IRectangleService
{
    public async Task<Rectangle?> GetRectangle() => await repository.GetRectangle();
    
    public async Task<Rectangle?> UpdateRectangle(Rectangle rectangle)
    {
        await validationService.EnsureValid(rectangle);

        return await repository.UpdateRectangle(rectangle);
    }
}