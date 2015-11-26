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
                //var group = new Group { Name = "net Maj 2015", Description = "Text text text text", StartDate = DateTime.ParseExact("2015-05-30", "yyyy-MM-dd"), EndDate = DateTime.ParseExact("2015-08-18", "yyyy-MM-dd") };

            }

            //if (!context.Groups.Any(g => g.Name == ".net Sep 2015"))
            //{
            //    var group = new Group { Name = "net Sep 2015", Description = "Text text text text", StartDate = DateTime("2013-09-01"), EndDate = new DateTime(2016,12,18)  };
            //}

            //if (!context.Groups.Any(g => g.Name == ".net Feb 2016"))
            //{
            //    var group = new Group { Name = "net Feb 2016", Description = "Text text text text", StartDate = new DateTime(2016,02,30), EndDate = new DateTime(2016,05,18) };
            //}
  
          //  StartDate = DateTime("2015-05-30", "yyyy-MM-dd", CultureInfo.InvariantCulture)


        //            public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public DateTime StartDate { get; set; }
        //public DateTime EndDate { get; set; }

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
