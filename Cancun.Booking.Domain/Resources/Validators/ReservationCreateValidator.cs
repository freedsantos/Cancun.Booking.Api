using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Helpers;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Cancun.Booking.Domain.Resources.Validators
{
    public class ReservationCreateValidator : AbstractValidator<ReservationCreateDto>
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IAvailabilityService _availabilityService;

        public ReservationCreateValidator(IRepository<Room> roomRepository,
                                          IRepository<User> userRepository,
                                          IAvailabilityService availabilityService,
                                          IOptions<BookingRules> options)
        {
            _roomRepository = roomRepository;
            _userRepository = userRepository;

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

            RuleFor(x => x!).Custom((obj, context) =>
            {
                var unavailableMessage = GetUnavailableDates(obj);

                if (!string.IsNullOrEmpty(unavailableMessage))
                    context.AddFailure(unavailableMessage);
            });
        }


        public bool ValidUser(int id)
        {
            return _userRepository.AnyAsync(x => x.Id == id).Result;
        }

        public bool ValidRoom(int id)
        {
            return _roomRepository.AnyAsync(x => x.Id == id).Result;
        }

        public string GetUnavailableDates(ReservationCreateDto reservationDto)
        {
            if (ValidRoom(reservationDto.RoomId))
            {
                var notAvaiableDates = _availabilityService.GetUnavailableDates(reservationDto.StartDate, reservationDto.EndDate, reservationDto.RoomId).Result;

                if (notAvaiableDates is null || !notAvaiableDates!.Any())
                    return string.Empty;
                else
                    return string.Format(MessagesHelper.DatesNotAvailable, string.Join(MessagesHelper.COMMA, notAvaiableDates.Select(m => m.ShortDateString)));
            }
            else
                return string.Format(MessagesHelper.NotFound, nameof(Room));
        }
    }
}
