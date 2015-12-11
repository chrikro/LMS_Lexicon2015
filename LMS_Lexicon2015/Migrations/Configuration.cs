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
                new Group { Name = ".net Mar 2015", Description = "dotNet-utbildning. Påbyggnad våren-sommaren 2015.", StartDate = new DateTime(2015,03,06), EndDate = new DateTime(2015,08,21)  },    
                new Group { Name = ".net Sep 2015", Description = "dotNet-utbildning. Påbyggnad hösten 2015.", StartDate = new DateTime(2015, 08, 31), EndDate = new DateTime(2016, 12, 20) },
                new Group { Name = "Java Sep 2015", Description = "Java-utbildning.", StartDate = new DateTime(2015, 08, 31), EndDate = new DateTime(2015, 12, 20) },
                new Group { Name = ".net Feb 2016", Description = "dotNet-utbildning. Påbyggnad våren 2016.", StartDate = new DateTime(2016, 02, 28), EndDate = new DateTime(2016, 06, 16) }
            };

            context.Groups.AddOrUpdate(g => g.Name, groups);
            context.SaveChanges();
            /////-------------------------------------------------------------------------------------------
            // Användare (med hash lösenord om det inte finns) alla har lösenord=hej999

            var store = new UserStore<ApplicationUser>(context);
            var UserManager = new UserManager<ApplicationUser>(store);

            //bäst testdata för kalle anka (niswa..) och jultomten, grupp[1] (net sep 2015), sql-kursen

            createNewUser(context, groups[0].Id, UserManager, "Christina", "Kaffekopp", "Elev", "chrikro129@gmail.com");
            createNewUser(context, groups[0].Id, UserManager, "Lucia", "da Roma", "Lärare", "lucia@roma.it"); 
            createNewUser(context, groups[1].Id, UserManager, "Kalle", "Anka", "Lärare", "nisaw99@hotmail.com");
            createNewUser(context, groups[1].Id, UserManager, "Anna-Karin", "Rönnegård", "Lärare", "a.ronnegard@gmail.com");
            createNewUser(context, groups[1].Id, UserManager, "Tomte", "von Nordpoolen", "Elev", "jultomten@nordpoolen.org");
            createNewUser(context, groups[1].Id, UserManager, "Leo", "Henning", "Elev", "fgfg2@dfdf.se");
            createNewUser(context, groups[1].Id, UserManager, "Oscar", "Gustavsson", "Elev", "gfggfg@dfdf.se");
            createNewUser(context, groups[1].Id, UserManager, "Lilly", "Bergqvist", "Elev", "sara2@ander.se");
            createNewUser(context, groups[1].Id, UserManager, "Ingvar", "Andersson", "Elev", "dfdfdfw88@hotmail.com");
            createNewUser(context, groups[2].Id, UserManager, "Kalle", "Andersson", "Elev", "Kalle@Kalle.se");
            createNewUser(context, groups[2].Id, UserManager, "Liam", "Ahlqvist", "Elev", "fff@fff.se");
            createNewUser(context, groups[2].Id, UserManager, "Donald", "Trump", "Elev", "donald.trump@muppetshow.com");
            createNewUser(context, groups[2].Id, UserManager, "Angela", "Merkel", "Lärare", "angela.merkel@yahoo.de");
            createNewUser(context, groups[2].Id, UserManager, "Kalle", "Lundvall", "Elev", "nille23@hotmail.com");
            createNewUser(context, groups[2].Id, UserManager, "Alfred", "Lejon", "Lärare", "ff@ff.se");
            createNewUser(context, groups[3].Id, UserManager, "Wilma", "Benjaminsson", "Lärare", "45@hotmail.com");
            createNewUser(context, groups[3].Id, UserManager, "Elsa", "Andersson", "Elev", "343@hej.se");

            context.SaveChanges();

            /////----------------------
            //kurser 
            // flest kurser till grupp[1] (.NET sep 2015)
            var courses = new[] {
                new CourseOccasion { Name = "csharp", Description = "Text text text text", StartDate = new DateTime(2015, 03, 11), EndDate = new DateTime(2015, 03, 16), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "Angular JS", Description = "Text text text text", StartDate = new DateTime(2015, 04, 27), EndDate = new DateTime(2015, 05, 10), GroupId = groups[0].Id  },
                new CourseOccasion { Name = "SQL", Description = "Språk för att manipulera data i en databas.", StartDate = new DateTime(2015, 08, 31), EndDate = new DateTime(2015, 09, 05), GroupId = groups[1].Id  },
                new CourseOccasion { Name = "Scrum", Description = "Utveckling enligt scrum. En metod att använda. Bra att kunna.", StartDate = new DateTime(2015, 09, 06), EndDate = new DateTime(2015, 09, 14), GroupId = groups[1].Id  },
                new CourseOccasion { Name = "Test", Description = "Test. Planering och genomförande av tester.", StartDate = new DateTime(2015, 09, 18), EndDate = new DateTime(2015, 10, 01), GroupId = groups[1].Id  },
                new CourseOccasion { Name = "Spelprogrammering", Description = "Trivsam tillämpning av programmering.", StartDate = new DateTime(2015, 10, 10), EndDate = new DateTime(2015, 12, 17), GroupId = groups[1].Id  },                
                new CourseOccasion { Name = "JQuery", Description = "Javascript. En särskild modul som finns på internet. Bra att kunna.", StartDate = new DateTime(2015, 09, 12), EndDate = new DateTime(2016, 12, 01), GroupId = groups[2].Id  },
                new CourseOccasion { Name = "Git/Versionshantering", Description = "Versionshantering i allmänhet och Git i synnerhet.", StartDate = new DateTime(2016, 12, 05), EndDate = new DateTime(2016, 12, 16), GroupId = groups[2].Id  }

            };

            context.CourseOccasions.AddOrUpdate(co => co.Name, courses);
            context.SaveChanges();

            /////----------------------
            //Aktiviteter 
            //obs end i seeden. Olika End-datum
            //det finns bara aktiviteter till kurser som ingår i grupp[1].
            var activitys = new[] {
                new Activity{ Name = activityTypes[0].Name, Description = "Vad är SQL? Intro", StartDate = new DateTime(2015,09,01), EndDate = new DateTime(2015,09,01), CourseId = courses[2].Id },
                new Activity{ Name = activityTypes[1].Name, Description = "För att öva sql.", StartDate = new DateTime(2015,09,02), EndDate = new DateTime(2016,09,03), CourseId = courses[2].Id },
                new Activity{ Name = activityTypes[2].Name, Description = "Om artefakter och samarbete", StartDate = new DateTime(2015,09,06), EndDate = new DateTime(2015,09,06), CourseId = courses[3].Id },
                new Activity{ Name = activityTypes[0].Name, Description = "Samtalsteknik. En övning i att vara trivsam.", StartDate = new DateTime(2015,09,07), EndDate = new DateTime(2015,09,12), CourseId = courses[3].Id },
                new Activity{ Name = activityTypes[0].Name, Description = "Testteknik lärs ut. Även andra aspekter av test tas upp under den här aktiviteten.", StartDate = new DateTime(2015,09,20), EndDate = new DateTime(2015,09,22), CourseId = courses[4].Id }
           };

            context.Activitys.AddOrUpdate(at => at.EndDate, activitys);
            context.SaveChanges();

            /////----------------------
            //Documents (document måste finnas i identitymodel för att seed av dok ska fungera.
            
            createNewDocument(context, groups[1].Id,null,null, "nisaw99@hotmail.com", "ReadMe.txt", "Ett nytt fint dokument som handlar om något.", new DateTime(2015, 09, 01));
            createNewDocument(context, groups[1].Id, courses[2].Id, null, "jultomten@nordpoolen.org", "csharp_extra.txt", "Ett nytt fint dokument som handlar om C#.", new DateTime(2015, 09, 02));
            createNewDocument(context, groups[1].Id, courses[2].Id, activitys[0].Id, "jultomten@nordpoolen.org", "test.txt", "Ett nytt fint dokument som handlar om SQL.", new DateTime(2015, 09, 03));
        }

        // ------------- separat metod för att skapa nya dokument. Anropas fr ovan -----------------------
        private static void createNewDocument(LMS_Lexicon2015.Models.ApplicationDbContext context, int? groupIdNewDoc, int? courseIdNewDoc, int? activitieIdNewDoc, string docOwnerNew, string docNamnNew, string docDescriptNew, DateTime docTimestampNew)
        {
            var documents = new[] {
                    new Document { Name = docNamnNew, 
                    Url = docNamnNew, 
                    Description = docDescriptNew, 
                    Timestamp = docTimestampNew, 
                    GroupId = groupIdNewDoc , 
                    UserId = context.Users.Where(u => u.UserName == docOwnerNew).FirstOrDefault().Id ,  
                    CourseOccasionId = courseIdNewDoc,
                    ActivityId = activitieIdNewDoc}
                };

            context.Documents.AddOrUpdate(d => d.Name, documents);
            context.SaveChanges();
        }

        // ------------- separat metod för att skapa användare. Anropas fr ovan -----------------------
        private static void createNewUser(LMS_Lexicon2015.Models.ApplicationDbContext context, int gruppId, UserManager<ApplicationUser> UserManager, string firstNameNew, string lastNameNew, string roleNew, string epostAdressNew)
        {
            if (UserManager.FindByEmail(epostAdressNew) == null)
            {
                var user = new ApplicationUser { UserName = epostAdressNew, Email = epostAdressNew, FirstName = firstNameNew, LastName = lastNameNew, GroupId = gruppId };
                UserManager.Create(user, "hej999");
                context.SaveChanges();
            }
            var roleKeeper = UserManager.FindByEmail(epostAdressNew);
            UserManager.AddToRole(roleKeeper.Id, roleNew);
            context.SaveChanges();
        }
    }
}
