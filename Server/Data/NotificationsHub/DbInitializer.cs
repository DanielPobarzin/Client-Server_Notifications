namespace Server.Data.NotificationsHub
{
    public class DbInitializer
    {
        public static void Initialize(NotificationsDbContext context)
        {
            context.Database.EnsureCreated();
        }

    }
}
