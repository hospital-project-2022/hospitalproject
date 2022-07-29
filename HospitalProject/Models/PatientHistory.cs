using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class PatientHistory
    {
        [Key]
        public int PatientHistoryID { get; set; }

        public string PatientDetails { get; set; }

        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        public virtual Patient Patient { get; set; }
    }
    public class PatientHistoryDto
    {
        public int PatientHistoryID { get; set; }

        public string PatientDetails { get; set; }

        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }
    }
}