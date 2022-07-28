using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HospitalProject.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal MedicinePrice { get; set; }

       
    }

    public class MedicineDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public decimal MedicinePrice { get; set; }

    }
}