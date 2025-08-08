using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class User
    {
        public int Id { get; set; }
        public string name {  get; set; }
        public string phone_number {  get; set; }
        public string address {  get; set; }
        [Required]
        public string email {  get; set; }
        [Required]
        public string password { get; set; }
    }
}