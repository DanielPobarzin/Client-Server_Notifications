namespace Server.Data
{
	public class DbInitializer
	{
		public static void Initialize(ServerDbContext context)
		{
			context.Database.EnsureCreated();
		}

	}
}
