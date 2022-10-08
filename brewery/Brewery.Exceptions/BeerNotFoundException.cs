namespace Brewery.Exceptions
{
    /// <summary>
    /// Custom exception for 'not found' beer.
    /// </summary>
    public class BeerNotFoundException : Exception
    {
        public BeerNotFoundException()
        {
        }

        /// <param name="innerException"></param>
        public BeerNotFoundException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}