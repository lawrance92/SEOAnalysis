using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeoAnalysis2.Models
{
    public class HomeModel
    {
        [Required]
        public string URL { get; set; }
        public bool Exist { get; set; }
        public List<string> words { get; set; }
    }
}