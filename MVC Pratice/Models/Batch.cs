using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Pratice.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public DateTime current_date { get; set; }
        public string batch_name {  get; set; }
        public int userId {  get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }

    }
}