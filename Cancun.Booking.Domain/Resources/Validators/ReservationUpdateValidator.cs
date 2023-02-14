using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Helpers;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Cancun.Booking.Domain.Resources.Validators
{
    public class ReservationUpdateValidator : AbstractValidator<ReservationUpdateDto>
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Reservation> _reservationRepository;

        private readonly IAvailabilityService _availabilityService;

        public ReservationUpdateValidator(IRepository<Room> roomRepository,
                                          IRepository<User> userRepository,
                                          IRepository<Reservation> reservationRepository,
                                          IAvailabilityService availabilityService,
                                          IOptions<BookingRules> options)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;

            _availabilityService = availabilityService;

            BookingRules _options = options.Value;

            RuleFor(x => x.EndDate.Date)
            .GreaterThanOrEqualTo(x => x.StartDate.Date)
            .WithMessage(MessagesHelper.EndDateGreaterThanStart);

            RuleFor(x => x.StartDate.Date)
            .GreaterThan(x => DatesHelper.GetMinReservationDate(_options.ReservationMinDaysAhead))
            .WithMessage(MessagesHelper.MinReservationDate);

            RuleFor(x => DatesHelper.GetInterval(x.StartDate, x.EndDate))
            .LessThan(_options.ReservationMaxDays)
            .WithMessage(string.Format(MessagesHelper.ReservationMaxDays, _options.ReservationMaxDays));

            RuleFor(x => x.EndDate.Date)
            .LessThanOrEqualTo(DatesHelper.GetMaxReservationDate(_options.ReservationMaxDaysAhead))
            .WithMessage(string.Format(MessagesHelper.MaxReservationDate, _options.ReservationMaxDaysAhead));

            RuleFor(x => x.UserId)
            .Must(ValidUser)
            .WithMessage(string.Format(MessagesHelper.NotFound, nameof(User)));

            RuleFor(x => x.RoomId)
            .Must(ValidRoom)
            .WithMessage(string.Format(MessagesHelper.NotFound, nameof(Room)));

            RuleFor(x => x.Id)
            .Must(ValidReservation)
            .WithMessage(string.Format(MessagesHelper.NotFound, nameof(Reservation)));

            RuleFor(x => x!).Custom((obj, context) =>
            {
                string msg = string.Empty;

                if (ValidReservation(obj.Id))
                    msg = GetUnavailableDates(obj);

                if (!string.IsNullOrEmpty(msg))
                    context.AddFailure(msg);
            });
        }

        private bool ValidUser(int id)
        {
            return _userRepository.AnyAsync(x => x.Id == id).Result;
        }

        private bool ValidRoom(int id)
        {
            return _roomRepository.AnyAsync(x => x.Id == id).Result;
        }

        private bool ValidReservation(int id)
        {
            return _reservationRepository.AnyAsync(x => x.Id == id).Result;
        }

        private string GetUnavailableDates(ReservationUpdateDto reservationDto)
        {
            if (ValidRoom(reservationDto.RoomId))
            {
                var notAvaiableDates = _availabilityService.GetUnavailableDates(reservationDto.StartDate, reservationDto.EndDate, reservationDto.RoomId).Result;

                notAvaiableDates = RemoveThisReservationDates(notAvaiableDates, reservationDto.Id);

                if (notAvaiableDates is null || !notAvaiableDates!.Any())
                    return string.Empty;
                else
                    return string.Format(MessagesHelper.DatesNotAvailable, string.Join(MessagesHelper.COMMA, notAvaiableDates.Select(m => m.ShortDateString)));
            }
            else
                return string.Format(MessagesHelper.NotFound, nameof(Room));
        }

        public IEnumerable<AvailabilityDto> RemoveThisReservationDates(IEnumerable<AvailabilityDto> notAvaiableDates, int reservationId)
        {
            var reservation = GetReservation(reservationId);
            var reservationDates = DatesHelper.GetDatesBetween(reservation.StartDate, reservation.EndDate);

            var result = notAvaiableDates.ToList();
            result.RemoveAll(x => reservationDates.Contains(x.Date));

            return result;
        }

        private Reservation GetReservation(int id)
        {
            return _reservationRepository.GetFirstOrDefaultAsync(x => x.Id == id).Result!;
        }
    }
}
