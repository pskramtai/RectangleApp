using RectangleApi.Domain.Models;
using RectangleApi.Presentation.Requests;

namespace RectangleApi.Presentation.Mappers;

public static class RequestMapperExtensions
{
    public static Rectangle ToModel(this RectangleRequest request) =>
        new(
            Width: request.Width!.Value,
            Height: request.Height!.Value
        );
}