using AutoMapper;
using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;

namespace Cancun.Booking.Domain.Resources.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<ReservationCreateDto, Reservation>()
                .ForMember(dest => dest.StartDate,
                 opt => opt.AddTransform(src => src.Date))
                .ForMember(dest => dest.EndDate,
                 opt => opt.AddTransform(src => src.Date))
                .ReverseMap();

            CreateMap<ReservationUpdateDto, Reservation>()
                .ForMember(dest => dest.StartDate,
                 opt => opt.AddTransform(src => src.Date))
                .ForMember(dest => dest.EndDate,
                 opt => opt.AddTransform(src => src.Date))
                .ReverseMap();
        }
    }
}
