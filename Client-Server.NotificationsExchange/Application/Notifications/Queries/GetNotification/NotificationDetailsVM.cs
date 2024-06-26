﻿using WebAPI.Application.Common.Mappings;
using AutoMapper;
using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application.Notifications.Queries.GetNotification
{
    public class NotificationDetailsVM : IMapWith<Notification>
    {
        public Guid Id { get; set; }
        public string content { get; set; }
        public double value { get; set; }
        public char quality { get; set; }
        public DateTime creationdate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationDetailsVM>()
                .ForMember(notificationDto => notificationDto.Id,
                     opt => opt.MapFrom(notification => notification.id))
                .ForMember(notificationDto => notificationDto.content,
                     opt => opt.MapFrom(notification => notification.content))
                .ForMember(notificationDto => notificationDto.value,
                     opt => opt.MapFrom(notification => notification.value))
                .ForMember(notificationDto => notificationDto.quality,
                     opt => opt.MapFrom(notification => notification.quality))
                .ForMember(notificationDto => notificationDto.creationdate,
                     opt => opt.MapFrom(notification => notification.creationdate));
        }
    }
}
