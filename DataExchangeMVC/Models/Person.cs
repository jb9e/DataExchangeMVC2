using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataExchangeMVC.Models
{
    public class Person
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DOB { get; set; }
        
        public string SSN { get; set; }

        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
    }
}