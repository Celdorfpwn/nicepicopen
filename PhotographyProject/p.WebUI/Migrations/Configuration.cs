namespace p.WebUI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using p.Database.Concrete.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<p.Database.Concrete.Repositories.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(p.Database.Concrete.Repositories.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /*foreach (var user in context.Photographers)
            {
                var entry = new UserProfilePicture
                {
                    UserId = user.Id,
                    Image = user.ProfilePicture
                };
                context.UserProfilePictures.Add(entry);
            }
            context.SaveChanges();*/
        }
    }
}
