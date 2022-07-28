using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProject.Models;
using System.Web.Script.Serialization;
using HospitalProject.Models.ViewModels;

namespace HospitalProject.Controllers
{
    public class MedicinesController : Controller
    {
        private static readonly HttpClient client;

        static MedicinesController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/");
        }
            
        // GET: Medicines/List
        public ActionResult List()
        {
            string url = "MedicinesData/ListMedicines";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<MedicineDto> medicines = response.Content.ReadAsAsync<IEnumerable<MedicineDto>>().Result;

            return View(medicines);
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Medicines/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Medicines/Create
        [HttpPost]
        public ActionResult Create(Medicine medicine)
        {
            string url = "medicinesdata/addmedicine/";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(medicine);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
           
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateMedicine ViewModel = new UpdateMedicine();
            string url = "MedicinesData/FindMedicine/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MedicineDto MedicineDto = response.Content.ReadAsAsync< MedicineDto>().Result;
            ViewModel.SelectedMedicine = MedicineDto;
            return View(ViewModel);
        }

        // POST: Medicines/Update/5
        [HttpPost]
        public ActionResult Update(int id, Medicine medicine)
        {
            string url = "MedicinesData/UpdateMedicine/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(medicine);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Medicines/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "MedicinesData/FindMedicine/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            MedicineDto selectedMedicines = response.Content.ReadAsAsync<MedicineDto>().Result;
            return View(selectedMedicines);
        }

        // POST: Medicines/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "MedicinesData/DeleteMedicine/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
