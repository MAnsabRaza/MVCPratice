using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MVC_Pratice.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime current_date { get; set; }
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }
        public int studentId {  get; set; }
        [ForeignKey("studentId")]
        public virtual Student Student { get; set; }
        public int courseId { get; set; }
        [ForeignKey("courseId")]
        public virtual Course Course { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
    }
}