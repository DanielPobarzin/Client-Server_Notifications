
namespace Server.Models
{
	public class ServerModel 
	{
		public Guid Id { get; set; }
		public string Content { get; set; }
		public double Value { get; set; }
		public char Quality { get; set; }
		public DateTime CreationDate { get; set; }
	}
}