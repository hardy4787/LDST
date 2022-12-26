using ErrorOr;

namespace LDST.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            description: "Email is already in use.");
    }

    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            description: "Invalid credentials."
        );
    }
    public static class City
    {
        public static Error NotFoundCity => Error.NotFound(
            description: "City does not exist.");
    }

    public static class Sport
    {
        public static Error NotFoundSport => Error.NotFound(
            description: "Sport does not exist.");
    }
    public static class Playground
    {
        public static Error NotFoundPlayground => Error.NotFound(
            description: "Playground does not exist.");
    }
}
