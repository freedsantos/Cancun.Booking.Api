using AutoMapper;
using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Handlers;
using Cancun.Booking.Domain.Resources.Helpers;
using Cancun.Booking.Domain.Resources.Validators;

namespace Cancun.Booking.Domain.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly ReservationCreateValidator _reservationCreateValidator;
        private readonly ReservationUpdateValidator _reservationUpdateValidator;
        private readonly IMapper _mapper;

        public ReservationService(IRepository<Reservation> reservationRepository,
                                  ReservationCreateValidator reservationCreateValidator,
                                  ReservationUpdateValidator reservationUpdateValidator,
                                  IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _reservationCreateValidator = reservationCreateValidator;
            _reservationUpdateValidator = reservationUpdateValidator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task<Reservation> Update(ReservationUpdateDto reservationDto)
        {
            var validationResult = await _reservationUpdateValidator.ValidateAsync(reservationDto);

            if (validationResult.IsValid)
            {
                var reservation = _mapper.Map<Reservation>(reservationDto);

                await _reservationRepository.UpdateAsync(reservation);
                return reservation;
            }
            else
                throw new UserFriendlyException(string.Join(MessagesHelper.PIPE, validationResult.Errors.Select(m => m.ErrorMessage)));
        }

        public async Task Delete(int id)
        {
            var entity = await _reservationRepository.GetFirstOrDefaultAsync(m => m.Id == id);
            if (entity is null) throw new UserFriendlyException(string.Format(MessagesHelper.NotFound, nameof(Reservation)));
            _reservationRepository.Delete(entity);
        }

        public async Task<Reservation> Create(ReservationCreateDto reservationDto)
        {
            var validationResult = await _reservationCreateValidator.ValidateAsync(reservationDto);

            if (validationResult.IsValid)
            {
                var reservation = _mapper.Map<Reservation>(reservationDto);

                reservation.Id = 0;
                await _reservationRepository.AddAsync(reservation);

                return reservation;
            }
            else
                throw new UserFriendlyException(string.Join(MessagesHelper.PIPE, validationResult.Errors.Select(m => m.ErrorMessage)));
        }
    }
}
