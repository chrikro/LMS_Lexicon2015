using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon2015.Models
{
    public class ActivityType
    {
        [Key]
        public string Name { get; set; }
    }
}