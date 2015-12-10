namespace LMS_Lexicon2015.Migrations
{
    using LMS_Lexicon2015.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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

            //Aktivitets typ // lägg till activityTypes 
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
                new Group { Name = ".net Maj 2015", Description = "Text text text text", StartDate = new DateTime(2015,11,20), EndDate = new DateTime(2015,12,24)  },
                new Group { Name = ".net Sep 2015", Description = "Text text text text", StartDate = new DateTime(2015, 12, 20), EndDate = new DateTime(2016, 12, 18) },
                new Group { Name = "Java Sep 2015", Description = "Text text text text", StartDate = new DateTime(2015, 11, 20), EndDate = new DateTime(2015, 12, 29) },
                new Group { Name = ".net Feb 2016", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2016, 06, 16) }
            };

            context.Groups.AddOrUpdate(g => g.Name, groups);
            context.SaveChanges();
            /////-------------------------------------------------------------------------------------------

            //Nytt skapa användare med hash lösenord om det inte finns  
            var store = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(store);

            //user 1
            if (UserManager.FindByEmail("nisaw99@hotmail.com") == null)   {
                var user = new ApplicationUser { UserName = "nisaw99@hotmail.com", Email = "nisaw99@hotmail.com", FirstName = "Kalle", LastName = "Anka", GroupId = groups[1].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            var roleKeeper = UserManager.FindByEmail("nisaw99@hotmail.com");
            UserManager.AddToRole(roleKeeper.Id, "Lärare");  

            //user 2
            if (UserManager.FindByEmail("chrikro129@gmail.com") == null) {
                var user = new ApplicationUser { UserName = "chrikro129@gmail.com", Email = "chrikro129@gmail.com", FirstName = "Christina", LastName = "XXX", GroupId = groups[0].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("chrikro129@gmail.com");
            UserManager.AddToRole(roleKeeper.Id, "Elev");

            //user 3
            if (UserManager.FindByEmail("jultomten@home.se") == null)             {
                var user = new ApplicationUser { UserName = "jultomten@nordpoolen.org", Email = "jultomten@nordpoolen.org", FirstName = "Tomte", LastName = "von Nordpoolen", GroupId = groups[1].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("jultomten@nordpoolen.org");
            UserManager.AddToRole(roleKeeper.Id, "Elev");

            //user 4
            if (UserManager.FindByEmail("lucia@roma.it") == null) {
                var user = new ApplicationUser { UserName = "lucia@roma.it", Email = "lucia@roma.it", FirstName = "Lucia", LastName = "von Roma", GroupId = groups[0].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("lucia@roma.it");
            UserManager.AddToRole(roleKeeper.Id, "Lärare");

            //user 5
            if (UserManager.FindByEmail("donald.trump@muppetshow.com") == null)
            {
                var user = new ApplicationUser { UserName = "donald.trump@muppetshow.com", Email = "donald.trump@muppetshow.com", FirstName = "Donald", LastName = "Trump", GroupId = groups[2].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("donald.trump@muppetshow.com");
            UserManager.AddToRole(roleKeeper.Id, "Elev");

            //user 6
            if (UserManager.FindByEmail("angela.merkel@yahoo.de") == null)
            {
                var user = new ApplicationUser { UserName = "angela.merkel@yahoo.de", Email = "angela.merkel@yahoo.de", FirstName = "Angela", LastName = "Merkel", GroupId = groups[0].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("angela.merkel@yahoo.de");
            UserManager.AddToRole(roleKeeper.Id, "Lärare");

            //user 7
            if (UserManager.FindByEmail("a.ronnegard@gmail.com") == null)
            {
                var user = new ApplicationUser { UserName = "a.ronnegard@gmail.com", Email = "a.ronnegard@gmail.com", FirstName = "Anna-Karin", LastName = "Rönnegård", GroupId = groups[1].Id };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            roleKeeper = UserManager.FindByEmail("a.ronnegard@gmail.com");
            UserManager.AddToRole(roleKeeper.Id, "Lärare");

            context.SaveChanges();
            /////----------------------
            //kurser
            var courses = new[] {
                new CourseOccasion { Name = "csharp", Description = "Text text text text", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2016, 06, 16), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "Angular JS", Description = "Text text text text", StartDate = new DateTime(2016, 02, 27), EndDate = new DateTime(2016, 06, 16), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "Testigen", Description = "Text text text text", StartDate = new DateTime(2016, 02, 26), EndDate = new DateTime(2016, 06, 17), GroupId = groups[1].Id  },
                new CourseOccasion { Name = "SQL", Description = "Text text text text", StartDate = new DateTime(2016, 02, 25), EndDate = new DateTime(2016, 06, 18), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "JQuery", Description = "Text text text text", StartDate = new DateTime(2015, 12, 27), EndDate = new DateTime(2016, 01, 02), GroupId = groups[2].Id  },
                new CourseOccasion { Name = "Git/Versionshantering", Description = "Text text text text", StartDate = new DateTime(2016, 01, 10), EndDate = new DateTime(2016, 06, 18), GroupId = groups[2].Id  },
                new CourseOccasion { Name = "Nuvarande", Description = "Hej Hopp", StartDate = new DateTime(2015, 12, 03), EndDate = new DateTime(2015, 12, 25), GroupId = groups[0].Id  }
            };

            context.CourseOccasions.AddOrUpdate(co => co.Name, courses);
            context.SaveChanges();

            /////----------------------
            //Aktiviteter 
                //obs end i seeden. Olika End-datum
            var activitys = new[] {
                new Activity{ Name = activityTypes[0].Name, Description = "text text", StartDate = new DateTime(2016,02,28), EndDate = new DateTime(2016,06,16), CourseId = courses[4].Id },
                new Activity{ Name = activityTypes[1].Name, Description = "text text", StartDate = new DateTime(2016,06,18), EndDate = new DateTime(2016,06,20), CourseId = courses[4].Id },
                new Activity{ Name = activityTypes[2].Name, Description = "text text", StartDate = new DateTime(2016,06,18), EndDate = new DateTime(2016,06,23), CourseId = courses[3].Id },
                new Activity{ Name = activityTypes[0].Name, Description = "text text", StartDate = new DateTime(2016,05,18), EndDate = new DateTime(2016,06,10), CourseId = courses[3].Id },
                new Activity{ Name = activityTypes[0].Name, Description = "text text", StartDate = new DateTime(2016,06,17), EndDate = new DateTime(2016,06,22), CourseId = courses[4].Id }
           };

            context.Activitys.AddOrUpdate(at => at.EndDate, activitys);
            context.SaveChanges();

             /////----------------------
            //Documents

            //lägg till public System.Data.Entity.DbSet<LMS_Lexicon2015.Models.Document> Documents { get; set; } i identityModels
            // för att lägga in Documents context
            var documents = new[] {
                    new Document { Name = "Test.docx", 
                    Url = "Files/gagibcqr.mbh.docx", 
                    Description = "text text", 
                    Timestamp = new DateTime(2015, 09, 30), 
                    GroupId = groups[0].Id , 
                    UserId = context.Users.Where(u => u.UserName == "nisaw99@hotmail.com").FirstOrDefault().Id },  
                };

            context.Documents.AddOrUpdate(d => d.Name, documents);
            context.SaveChanges();
        }
    }
}




