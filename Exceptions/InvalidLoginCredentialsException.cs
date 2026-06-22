namespace BlogAPI.Exceptions;

public class InvalidLoginCredentialsException(string message) : AppException(message, StatusCodes.Status401Unauthorized) { }