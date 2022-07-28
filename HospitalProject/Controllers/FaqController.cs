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
    public class FaqController : Controller
    {

        private static readonly HttpClient client;

        static FaqController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/");
        }

        // GET: Medicines/List
        public ActionResult List()
        {
            string url = "FaqsData/ListFaqs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<FaqDto> faqs = response.Content.ReadAsAsync<IEnumerable<FaqDto>>().Result;

            return View(faqs);
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
        public ActionResult Create(Faq faq)
        {
            string url = "faqsdata/addfaq/";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(faq);
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
            UpdateFaq ViewModel = new UpdateFaq();
            string url = "FaqsData/FindFaq/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FaqDto faqDto = response.Content.ReadAsAsync<FaqDto>().Result;
            ViewModel.SelectedFaq = faqDto;
            url = "medicinesdata/listmedicines";
            response = client.GetAsync(url).Result;
            IEnumerable<MedicineDto> MedicineOptions = response.Content.ReadAsAsync<IEnumerable<MedicineDto>>().Result;

            ViewModel.MedicineOptions = MedicineOptions;
            return View(ViewModel);
        }

        // POST: Medicines/Update/5
        [HttpPost]
        public ActionResult Update(int id, Faq faq)
        {
            string url = "FaqsData/UpdateFaq/" + id;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(faq);
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
            string url = "FaqsData/FindFaq/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FaqDto selectedFaqs = response.Content.ReadAsAsync<FaqDto>().Result;
            return View(selectedFaqs);
        }

        // POST: Medicines/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "FaqsData/DeleteFaq/" + id;
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
