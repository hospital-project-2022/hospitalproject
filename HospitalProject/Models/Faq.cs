using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Faq
    {
        [Key]
        public int FaqId { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }

        [ForeignKey("Medicine")]
        public int? MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }

    }

    public class FaqDto
    {
        public int FaqId { get; set; }
        public string FaqQuestion { get; set; }
        public string FaqAnswer { get; set; }
        public string MedicineName { get; set; }
        public decimal MedicinePrice { get; set; }

    }
}