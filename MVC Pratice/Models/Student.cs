using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class Student
    {
        public int Id { get; set; }
        public DateTime current_date { get; set; }
        public string student_name {  get; set; }
        public int age {  get; set; }
        public string address {  get; set; }
        public int userId {  get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }
        public int courseId { get; set; }
        [ForeignKey("courseId")]
        public virtual Course Course { get; set; }
        public int batchId {  get; set; }
        [ForeignKey("batchId")]
        public virtual Batch Batch { get; set; }
    }
}