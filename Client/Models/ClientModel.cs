using Client.Mappings;
using AutoMapper;
using Server.Models;

namespace Client.Models
{
	public class ClientModel : IMapWith<ServerModel>
	{
		public Guid Id { get; set; }
		public Guid ServerId { get; set; }
		public Guid ClientId { get; set; }
		public string Content { get; set; }
		public double Value { get; set; }
		public char Quality { get; set; }
		public DateTime CreationDate { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<ServerModel, ClientModel>()
				.ForMember(client => client.Id,
					 opt => opt.MapFrom(server => server.Id))
				.ForMember(client => client.Content,
					 opt => opt.MapFrom(server => server.Content))
				.ForMember(client => client.Value,
					 opt => opt.MapFrom(server => server.Value))
				.ForMember(client => client.Quality,
					 opt => opt.MapFrom(server => server.Quality))
				.ForMember(client => client.CreationDate,
					 opt => opt.MapFrom(server => server.CreationDate));
		}
	}
}