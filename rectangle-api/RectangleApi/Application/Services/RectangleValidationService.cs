using RectangleApi.Domain.Exceptions;
using RectangleApi.Domain.Models;
using RectangleApi.Domain.Services;

namespace RectangleApi.Application.Services;

public class RectangleValidationService : IRectangleValidationService
{
    public async Task EnsureValid(Rectangle rectangle)
    {
        // Long running "operation"
        await Task.Delay(10000);

        if (rectangle.Width > rectangle.Height)
        {
            throw new ValidationException($"Rectangle width ({rectangle.Width}) is greater height ({rectangle.Height})");
        }
    }
}