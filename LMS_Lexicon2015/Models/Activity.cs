using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace LMS_Lexicon2015.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "{0} måste vara minst {2} tecken och max 1000 tecken långt.", MinimumLength = 3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Kursid")]
        [ForeignKey("CourseOccasion")]
        public int CourseId { get; set; }

        public virtual CourseOccasion CourseOccasion { get; set; } 
    }
}


