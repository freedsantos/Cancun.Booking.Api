namespace Cancun.Booking.Domain.Resources.Helpers
{
    public static class MessagesHelper
    {
        public const string NotFound = "{0} not found.";
        public const string EndDateGreaterThanStart = "End Date must be greater than Start Date.";
        public const string MinReservationDate = "Reservation has to start at least next day of booking.";
        public const string ReservationMaxDays = "Reservation can't have more than {0} days.";
        public const string MaxReservationDate = "Reservation can't be more than {0} days ahead.";
        public const string DatesNotAvailable = "Following date(s) are not available: {0}";

        public const string COMMA = ", ";
        public const string PIPE = " | ";
    }
}
