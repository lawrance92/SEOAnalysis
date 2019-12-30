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
        // Home model for store the information and pare to view to process
        [Required]
        public string URL { get; set; } // this field is required and cannot be empty
        public bool Exist { get; set; } // Assign boolean value for website existing or not
        public List<string> words { get; set; } // Assign the list of stop-words which is match with websites content
    }
}