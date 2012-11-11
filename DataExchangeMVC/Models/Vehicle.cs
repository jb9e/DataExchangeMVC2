using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataExchangeMVC.Models
{
    public class Vehicle
    {
        public int ID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public string Color { get; set; }

        [Required]
        public string Plate { get; set; }

        public int? Year { get; set; }
    }
}