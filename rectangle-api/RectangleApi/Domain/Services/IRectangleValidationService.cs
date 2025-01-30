using RectangleApi.Domain.Models;

namespace RectangleApi.Domain.Services;

public interface IRectangleValidationService
{
    Task EnsureValid(Rectangle rectangle);
}