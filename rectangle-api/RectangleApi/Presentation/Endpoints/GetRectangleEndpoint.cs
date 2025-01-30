using RectangleApi.Domain.Services;
using RectangleApi.Presentation.Mappers;

namespace RectangleApi.Presentation.Endpoints;

public static class GetRectangleEndpoint
{
    public static WebApplication RegisterGetRectangleEndpoint(this WebApplication app)
    {
        app
            .MapGet("/rectangle/", Handler)
            .Produces(404)
            .Produces(400);

        return app;
    }

    private static async Task<IResult> Handler(IRectangleService service)
    {
        var rectangle = await service.GetRectangle();

        return rectangle is not null
            ? Results.Ok(rectangle.ToResponse()) 
            : Results.NotFound();
    }
}