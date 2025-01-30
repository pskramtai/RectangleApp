namespace RectangleApi.Domain.Exceptions;

public class ValidationException(string message) : Exception(message);