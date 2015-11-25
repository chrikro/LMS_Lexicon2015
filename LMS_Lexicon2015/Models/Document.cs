using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_Lexicon2015.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime? Deadline { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int courseId { get; set; }
        public int ActivityId { get; set; }
    }
}



