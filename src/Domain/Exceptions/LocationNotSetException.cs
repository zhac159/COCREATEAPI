namespace Domain.Exceptions;

public class LocationNotSetException : Exception
{
    private const string message = "location-not-set";
    public LocationNotSetException()
        : base(message) { }
}
