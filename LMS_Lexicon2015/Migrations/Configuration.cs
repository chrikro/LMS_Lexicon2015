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
            foreach (string roles in new[] { "Elev", "L�rare" })
            {
                if (!context.Roles.Any(u => u.Name == roles))
                {
                    var role = new IdentityRole { Name = roles };
                    roleManager.Create(role);
                }
            }

            var groups = new[] {
                new Group { Name = ".net Maj 2015", Description = "Text text text text", StartDate = new DateTime(2015,05,30), EndDate = new DateTime(2015,08,18)  },
                new Group { Name = ".net Sep 2015", Description = "Text text text text", StartDate = new DateTime(2015, 08, 30), EndDate = new DateTime(2015, 12, 18) },
                new Group { Name = ".net Feb 2016", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16) }
            };

            context.Groups.AddOrUpdate(g => g.Name, groups);
            context.SaveChanges();


            //Nytt skapa anv�ndare med hash l�senord om det inte finns  
            var store = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(store);


            if (UserManager.FindByEmail("nisaw99@hotmail.com") == null)
            {
                var user = new ApplicationUser { UserName = "nisaw99@hotmail.com", Email = "nisaw99@hotmail.com", FirstName = "Kalle", LastName = "Anka", GroupId = null };
                UserManager.Create(user, "hej999");// foobar = hej999
            }

            var keeper = UserManager.FindByName("nisaw99@hotmail.com");
            UserManager.AddToRole(keeper.Id, "L�rare");  //L�gg till en roll f�r nisaw99@hotmail.com // AddToRole l�gger in en ny roll men inte om den redan finns

            if (!context.CourseOccasions.Any(c => c.Name == "c#"))
            {
                var CourseOccasion = new CourseOccasion { Name = "c#", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = groups[0].Id };
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
