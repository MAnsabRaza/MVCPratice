using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public DateTime current_date {  get; set; }
        public int studentId {  get; set; }
        [ForeignKey("studentId")]
        public virtual Student Student { get; set; }
        public int courseId { get; set; }
        [ForeignKey("courseId")]
        public virtual Course Course { get; set; }
    }
}