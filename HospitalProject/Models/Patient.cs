using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class PatientDto
    {
        public int PatientID { get; set; }

        public string PatientName { get; set; }

        public string Sex { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
    }
}