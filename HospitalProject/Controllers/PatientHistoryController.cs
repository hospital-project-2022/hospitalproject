using HospitalProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalProject.Controllers
{
    public class PatientHistoryController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PatientHistoryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/PatientHistoryData/");
        }

        // GET: PatientHistory
        public ActionResult Index()
        {
            return View();
        }


        // GET: PatientHistory/Details/5
        public ActionResult Details(int id)
        {

            string url = "FindPatientHistory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientHistoryDto> selectedPatientHistory = response.Content.ReadAsAsync<IEnumerable<PatientHistoryDto>>().Result;

            return View(selectedPatientHistory);
        }


        // GET: PatientHistory/New
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: PatientHistory/Create
        [HttpPost]
        public ActionResult Create(PatientHistory patienthistory)
        {
            string url = "AddPatientHistory";


            string jsonpayload = jss.Serialize(patienthistory);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Patient");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: PatientHistory/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindOnePatientHistory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientHistoryDto selectedPatientHistory = response.Content.ReadAsAsync<PatientHistoryDto>().Result;
            Debug.WriteLine(selectedPatientHistory);
            return View(selectedPatientHistory);
        }

        // POST: PatientHistory/Update/5
        [HttpPost]
        public ActionResult Update(int id, PatientHistory patienthistory)
        {
            string url = "UpdatePatientHistory/" + id;
            string jsonpayload = jss.Serialize(patienthistory);
            Debug.WriteLine("content" + jsonpayload);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return View("Index");
            }
            else
            {
                return View("Error");
            }
        }

        // GET: PatientHistory/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindOnePatientHistory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientHistoryDto selectedPatientHistory = response.Content.ReadAsAsync<PatientHistoryDto>().Result;
            return View(selectedPatientHistory);
        }


        // POST: PatientHistory/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeletePatientHistory/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
