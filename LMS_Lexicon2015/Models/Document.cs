using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMS_Lexicon2015.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Url { get; set; }

        [Display(Name = "Beskrivning")]
        public string Description { get; set; }

        [Display(Name = "Tidsstämpel")]
        public DateTime Timestamp { get; set; }

        [Display(Name = "Sista inlämningstid")]
        public DateTime? Deadline { get; set; }
        
        [Display(Name = "Användarnamn")]
        public int UserId { get; set; }

        [Display(Name = "GruppId")]
        public int GroupId { get; set; }

        [Display(Name = "KursId")]
        public int courseId { get; set; }

        [Display(Name = "AktivitetsId")]
        public int ActivityId { get; set; }
    }
}


//public class DownloadResult : ActionResult
//{

//    public DownloadResult()
//    {
//    }

//    public DownloadResult(string virtualPath)
//    {
//        this.VirtualPath = virtualPath;
//    }

//    public string VirtualPath {get; set;   }

//    public string FileDownloadName { get; set; }

//    public override void ExecuteResult(ControllerContext context) {
//    if (!String.IsNullOrEmpty(FileDownloadName)) {
//      context.HttpContext.Response.AddHeader("content-disposition", 
//        "attachment; filename=" + this.FileDownloadName)
//    }

//    string filePath = context.HttpContext.Server.MapPath(this.VirtualPath);
//    context.HttpContext.Response.TransmitFile(filePath);
//    }
//}
