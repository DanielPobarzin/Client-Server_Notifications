using System;
using WebAPI.Application.Common.Mappings;
using WebAPI.Application.Notifications.Commands.UpdateNotifications;
using MediatR;
using AutoMapper;
using WebAPI.Application.Notifications.Commands.CreateNotifications;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class UpdateNotificationsDTO : IMapWith<UpdateNotificationsCommand>
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
		public double Value { get; set; }
		public char Quality { get; set; }
		public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNotificationsDTO, UpdateNotificationsCommand>()
                .ForMember(noteCommand => noteCommand.Id,
                opt => opt.MapFrom(noteDto => noteDto.Id))
				.ForMember(noteCommand => noteCommand.Content,
				opt => opt.MapFrom(noteDto => noteDto.Content))
				.ForMember(noteCommand => noteCommand.Value,
				 opt => opt.MapFrom(noteDto => noteDto.Value))
				.ForMember(noteCommand => noteCommand.Quality,
				opt => opt.MapFrom(noteDto => noteDto.Quality));
		}
    }
}