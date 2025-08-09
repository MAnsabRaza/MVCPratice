using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class Course
    {
        public int Id { get; set; }
        public DateTime current_date { get; set; }
        public string course_name { get; set; }
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }
    }
}