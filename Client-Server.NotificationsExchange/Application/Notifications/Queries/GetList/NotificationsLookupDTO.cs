using System;
using AutoMapper;
using WebAPI.Application.Common.Mappings;
using WebAPI.Domain;
using MediatR;


namespace WebAPI.Application.Notifications.Queries.GetList
{

    public class NotificationsLookupDTO : IMapWith<Notification>
    {
        public Guid id { get; set; }
        public string content { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationsLookupDTO>()
                .ForMember(noteDto => noteDto.id,
                     opt => opt.MapFrom(note => note.id))
                .ForMember(noteDto => noteDto.content,
                     opt => opt.MapFrom(note => note.content));
        }

    }
}
