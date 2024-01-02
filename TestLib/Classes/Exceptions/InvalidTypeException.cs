namespace TestLib.Classes.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message) : base(message)
        {
        }

        public InvalidTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidTypeException()
        {
        }

        public InvalidTypeException(string message, int errorCode) : base(message)
        {
            HResult = errorCode;
        }
    }
}
