namespace WebAPI.Persistance
{
	public class DbInitializer
	{
		public static void Initialize(NotificationDbContext context)
		{
			context.Database.EnsureCreated();
		}

	}
}
