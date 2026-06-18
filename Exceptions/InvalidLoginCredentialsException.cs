namespace BlogAPI.Exceptions;

public class InvalidLoginCredentialsException(string message) : Exception(message) { }