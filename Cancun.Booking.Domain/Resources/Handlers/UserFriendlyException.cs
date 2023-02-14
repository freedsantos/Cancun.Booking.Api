namespace Cancun.Booking.Domain.Resources.Handlers
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException() { }

        public UserFriendlyException(string message) : base(message) { }

        public UserFriendlyException(string message, Exception inner) : base(message, inner) { }

        public override string StackTrace
        {
            get
            {
                return null!;
            }
        }
    }
}
