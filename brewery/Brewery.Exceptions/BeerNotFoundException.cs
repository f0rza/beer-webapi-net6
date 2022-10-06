namespace Brewery.Exceptions
{
    public class BeerNotFoundException : Exception
    {
        public BeerNotFoundException()
        {
        }

        public BeerNotFoundException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}