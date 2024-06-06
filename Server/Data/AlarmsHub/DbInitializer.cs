namespace Server.Data.AlarmsHub
{
	public class DbInitializer
	{
		public static void Initialize(AlarmsDbContext context)
		{
			context.Database.EnsureCreated();
		}

	}
}
