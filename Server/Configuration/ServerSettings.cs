using System.Security;
using System.Text.Json.Serialization;

namespace Server.Configuration
{
	public class NotificationsHubSettings
	{
	    [JsonPropertyName("DelayMilliseconds")]
		public int DelayMilliseconds { get; set; }
		[JsonPropertyName("ServerId")]
		public Guid ServerId { get; set; }
	    [JsonPropertyName("UseCompareLists")]
		public bool UseCompareLists { get; set; }
		[JsonPropertyName("HubMethod")]
		public string HubMethod { get; set; }
		[JsonPropertyName("TargetClients")]
		public string TargetClients {  get; set; }
	}

	public class ServiceSettings
	{
		[JsonPropertyName("KeepAliveInterval")]
		public TimeSpan KeepAliveInterval { get; set; }
		[JsonPropertyName("EnableDetailedErrors")]
		public bool EnableDetailedErrors { get; set; }
		[JsonPropertyName("PolicyName")]
		public string PolicyName { get; set; }
		[JsonPropertyName("AllowedHeaders")]
		public string[] AllowedHeaders { get; set; }
		[JsonPropertyName("AllowedOrigins")]
		public string[] AllowedOrigins { get; set; }
		[JsonPropertyName("AllowedMethods")]
		public string[] AllowedMethods { get; set; }
	}

	public class TransportSettings 
	{
		[JsonPropertyName("CloseTimeout")]
		public TimeSpan CloseTimeout { get; set; }
		[JsonPropertyName("UseCorsName")]
		public string UseCorsName { get; set; }
		[JsonPropertyName("Route ")]
		public string Route { get; set; }
		[JsonPropertyName("TransportMaxBufferSize")]
		public int TransportMaxBufferSize { get; set; }
		[JsonPropertyName("TypeTransport")]
		public string TypeTransport { get; set; }
		[JsonPropertyName("CloseTimeOut")]
		public TimeOnly CloseTimeOut { get; set; }

	}
}
