namespace DataAccess.Model.Context.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessContext context)
        {
            var dataBaseInitializer = new DataBaseInitializer(context);
            dataBaseInitializer.Init(context);
        }
    }
}
