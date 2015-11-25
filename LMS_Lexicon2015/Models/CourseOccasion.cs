using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Lexicon2015.Models
{
    public class CourseOccasion
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupId { get; set; }
    }
}


