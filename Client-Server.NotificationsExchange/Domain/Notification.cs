using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
	public class Notification
	{
		public Guid id { get; set; }
		public string content { get; set; }
		public double value { get; set; }
		public char quality { get; set; }
		public DateTime creationdate { get; set; }
	}
}
