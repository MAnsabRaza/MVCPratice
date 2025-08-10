using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public DateTime current_date {  get; set; }
        public int studentId {  get; set; }
        [ForeignKey("studentId")]
        public virtual Student Student { get; set; }
        public int courseId {  get; set; }
        [ForeignKey("courseId")]
        public virtual Course Course { get; set; }
        public int userId {  get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }
        public string exam_type {  get; set; }
        public string marks {  get; set; }

    }
}