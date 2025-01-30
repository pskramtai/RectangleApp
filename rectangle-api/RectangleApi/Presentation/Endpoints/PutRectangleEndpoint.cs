using RectangleApi.Domain.Exceptions;
using RectangleApi.Domain.Services;
using RectangleApi.Presentation.Mappers;
using RectangleApi.Presentation.Requests;

namespace RectangleApi.Presentation.Endpoints;

public static class PutRectangleEndpoint
{
    public static WebApplication RegisterPutRectangleEndpoint(this WebApplication app)
    {
        app.MapPut("/rectangle/", Handler);

        return app;
    }

    private static async Task<IResult> Handler(IRectangleService service, RectangleRequest request)
    {
        if (request.Width is null || request.Height is null)
        {
            return Results.BadRequest();
        }
        
        var rectangle = request.ToModel();

        try
        {
            var updatedRectangle = await service.UpdateRectangle(rectangle);

            return updatedRectangle is not null
                ? Results.Ok(rectangle.ToResponse())
                : Results.NotFound();
        }
        catch (ValidationException e)
        {
            return Results.BadRequest(e.ToResponse());
        }
        catch
        {
            return Results.InternalServerError();
        }
    }
}