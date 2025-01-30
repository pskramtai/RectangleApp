using RectangleApi.Domain.Exceptions;
using RectangleApi.Domain.Models;
using RectangleApi.Presentation.Responses;

namespace RectangleApi.Presentation.Mappers;

public static class ResponseMapperExtensions
{
    public static RectangleResponse ToResponse(this Rectangle model) =>
        new(
            Width: model.Width,
            Height: model.Height
        );

    public static BadRequestResponse ToResponse(this ValidationException exception) =>
        new(exception.Message);
}