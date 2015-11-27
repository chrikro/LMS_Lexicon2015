﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon2015.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

       [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

       [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Display(Name = "GruppId")]
        public int? GroupId { get; set; }

        [Display(Name = "Grupp")]
        public virtual Group Group { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here

   //         userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, FirstName));

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<LMS_Lexicon2015.Models.Group> Groups { get; set; }

        //public System.Data.Entity.DbSet<LMS_Lexicon2015.Models.ApplicationUser> ApplicationUsers { get; set; }

        //public System.Data.Entity.DbSet<LMS_Lexicon2015.Models.ApplicationUser> ApplicationUsers { get; set; }

         
    }
}