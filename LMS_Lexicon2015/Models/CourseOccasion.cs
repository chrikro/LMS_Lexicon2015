using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon2015.Models
{
    public class CourseOccasion
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 3)]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Slutdatum")]
        public DateTime EndDate { get; set; }

        //[Display(Name = "GruppId")]
        //public int GroupId { get; set; }

        [Display(Name = "Grupp")]
        public int? GroupId { get; set; }

        [Display(Name = "Grupp")]
        public virtual Group Group { get; set; }

    }
}


