namespace CarRentalSystem.Core.Shared;

public static class DomainErrors
{
    public static readonly Error InternalServerError = new(
        "Server.InternalServerError",
        "An internal error happened during operation");

    public static readonly Error UserWithEmailNotFound = new(
        "User.EmailNotFound",
        "User with the given email address does not exist");

    public static readonly Error UserWithIdNotFound = new(
        "User.IdNotFound",
        "User with the given ID does not exist");

    public static readonly Error UserEmailNotConfirmed = new(
        "User.EmailNotConfirmed",
        "Email address must be confirmed first");

    public static readonly Error UserCredentialsInvalid = new(
        "User.InvalidCredentials",
        "User credentials are invalid");
}