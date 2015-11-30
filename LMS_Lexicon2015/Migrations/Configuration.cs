namespace LMS_Lexicon2015.Migrations
{
    using LMS_Lexicon2015.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_Lexicon2015.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LMS_Lexicon2015.Models.ApplicationDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            //foreach (string roles in new[] { "Studien", "Teatcher"})
            foreach (string roles in new[] { "Elev", "Lärare" })
            {
                if (!context.Roles.Any(u => u.Name == roles))
                {
                    var role = new IdentityRole { Name = roles };
                    roleManager.Create(role);
                }
            }

            //Nytt skapa användare med hash lösenord om det inte finns  
            var store = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(store);


            if (!context.Users.Any(u => u.UserName == "nisaw99@hotmail.com"))
                {
                    var user = new ApplicationUser { UserName = "nisaw99@hotmail.com", Email = "nisaw99@hotmail.com", FirstName = "Kalle", LastName = "Anka", GroupId = null};
                    UserManager.Create(user, "hej999");// foobar = hej999
                }

            var keeper = UserManager.FindByName("nisaw99@hotmail.com");
            UserManager.AddToRole(keeper.Id, "Lärare");  //Lägg till en roll för nisaw99@hotmail.com // AddToRole lägger in en ny roll men inte om den redan finns

            if (!context.Groups.Any(g => g.Name == ".net Maj 2015"))
            {
                var group = new Group { Name = ".net Maj 2015", Description = "Text text text text", StartDate = new DateTime(2015,05,30), EndDate = new DateTime(2015,08,18)  };
                context.Groups.Add(group);
            }

            if (!context.Groups.Any(g => g.Name == ".net Sep 2015"))
            {
                var group = new Group { Name = ".net Sep 2015", Description = "Text text text text", StartDate = new DateTime(2015, 08, 30), EndDate = new DateTime(2015, 12, 18) };
                context.Groups.Add(group);
            }

            if (!context.Groups.Any(g => g.Name == ".net Feb 2016"))
            {
                var group = new Group { Name = ".net Feb 2016", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16) };
                context.Groups.Add(group);
            }

            if (!context.CourseOccasions.Any(c => c.Name == "c#"))
            {
                var CourseOccasion = new CourseOccasion { Name = "c#", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = 1 };
                context.CourseOccasions.Add(CourseOccasion);
            }




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
        }
    }
}
