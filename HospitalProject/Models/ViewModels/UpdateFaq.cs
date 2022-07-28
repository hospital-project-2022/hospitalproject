using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateFaq
    {
        public FaqDto SelectedFaq { get; set; }
        public IEnumerable<MedicineDto> MedicineOptions { get; set; }
    }
}