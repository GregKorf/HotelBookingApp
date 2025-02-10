namespace HotelBookingApp.Exceptions
{
    public class InvalidOperationException : AppException
    {
        private static readonly string DEFAULT_CODE = "";

        public InvalidOperationException(string code, string message)
            : base(code + DEFAULT_CODE, message)
        {
        }
    }
}
