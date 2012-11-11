using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DataExchangeMVC.Models
{
    public class Log
    {
        public int ID { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string Message { get; set; }
    }
}