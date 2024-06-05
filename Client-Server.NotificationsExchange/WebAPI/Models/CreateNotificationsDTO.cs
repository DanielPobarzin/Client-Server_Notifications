using AutoMapper;
using WebAPI.Application.Common.Mappings;
using WebAPI.Application.Notifications.Commands.CreateNotifications;
using System.ComponentModel.DataAnnotations;
namespace WebAPI.Models
{
    public class CreateNotificationsDTO : IMapWith<CreateNotificationsCommand>
    {
        public string Content { get; set; }
        public double Value { get; set; }
		public char Quality { get; set; }
		public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNotificationsDTO, CreateNotificationsCommand>()
                .ForMember(noteCommand => noteCommand.Content,
                     opt => opt.MapFrom(noteDto => noteDto.Content))
                .ForMember(noteCommand => noteCommand.Value,
                     opt => opt.MapFrom(noteDto => noteDto.Value))
                .ForMember(noteCommand => noteCommand.Quality,
					 opt => opt.MapFrom(noteDto => noteDto.Quality));
		}
    }
}
