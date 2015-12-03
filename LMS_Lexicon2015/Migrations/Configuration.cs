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

            foreach (string roles in new[] { "Elev", "Lärare" })
            {
                if (!context.Roles.Any(r => r.Name == roles))
                {
                    var role = new IdentityRole { Name = roles };
                    roleManager.Create(role);
                }
            }
            /////----------------------

            //Aktivitets typ // lägg till activityTypes i IdentityModels
            var activityTypes = new[] {
                new ActivityType { Name = "Föreläsning" },
                new ActivityType { Name = "E-learning" },
                new ActivityType { Name = "ÖvningsUppgift" },
                new ActivityType { Name = "Projekt" }
           };

            context.ActivityTypes.AddOrUpdate(at => at.Name, activityTypes);
            context.SaveChanges();
            /////----------------------
            //grupper
            var groups = new[] {
                new Group { Name = ".net Maj 2015", Description = "Text text text text", StartDate = new DateTime(2015,05,30), EndDate = new DateTime(2015,08,18)  },
                new Group { Name = ".net Sep 2015", Description = "Text text text text", StartDate = new DateTime(2015, 08, 30), EndDate = new DateTime(2015, 12, 18) },
                new Group { Name = ".net Feb 2016", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16) }
            };

            context.Groups.AddOrUpdate(g => g.Name, groups);
            context.SaveChanges();
            /////----------------------

            //Nytt skapa användare med hash lösenord om det inte finns  
            var store = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(store);


            if (UserManager.FindByEmail("nisaw99@hotmail.com") == null)
            {
                var user = new ApplicationUser { UserName = "nisaw99@hotmail.com", Email = "nisaw99@hotmail.com", FirstName = "Kalle", LastName = "Anka", GroupId = null };
                UserManager.Create(user, "hej999");// foobar = hej999
                context.SaveChanges();
            }
            context.SaveChanges();

            var roleKeeper = UserManager.FindByEmail("nisaw99@hotmail.com");
            UserManager.AddToRole(roleKeeper.Id, "Lärare");  //Lägg till en roll för nisaw99@hotmail.com // AddToRole lägger in en ny roll men inte om den redan finns

            if (UserManager.FindByEmail("chrikro129@gmail.com") == null)
            {
                var user = new ApplicationUser { UserName = "chrikro129@gmail.com", Email = "chrikro129@gmail.com", FirstName = "Christina", LastName = "Kronblad", GroupId = groups[1].Id };
                UserManager.Create(user, "hej999");
            }
            context.SaveChanges();

            roleKeeper = UserManager.FindByEmail("chrikro129@gmail.com");
            UserManager.AddToRole(roleKeeper.Id, "Elev");

            /////----------------------
            //kurser
            var courses = new[] {
                new CourseOccasion { Name = "c#", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = groups[0].Id  },
                 new CourseOccasion { Name = "Angular JS", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "Test", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = groups[1].Id  },
                new CourseOccasion { Name = "SQL", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), GroupId = groups[0].Id  }
            };

            context.CourseOccasions.AddOrUpdate(co => co.Name, courses);
            context.SaveChanges();

            /////----------------------

            //Aktiviteter // lägg till activitys i IdentityModels
            var activitys = new[] {
                new Activity{ Name = activityTypes[0].Name, Description = "text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), CourseId = courses[0].Id},
                new Activity{ Name = activityTypes[0].Name, Description = "text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2015, 06, 16), CourseId = courses[0].Id}
           };

            context.Activitys.AddOrUpdate(at => at.Name, activitys);
            context.SaveChanges();
        }
    }
}
