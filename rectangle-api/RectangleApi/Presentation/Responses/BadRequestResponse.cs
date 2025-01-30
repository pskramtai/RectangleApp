namespace RectangleApi.Presentation.Responses;

public record BadRequestResponse
(
    string Message,
    string Reason = "Bad request"
);