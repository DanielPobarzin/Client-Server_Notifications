namespace Client.Data
{
    public class DbInitializer
    {
        public static void Initialize(ClientDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
