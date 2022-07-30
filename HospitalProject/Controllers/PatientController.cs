using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44300/api/PatientData/");
        }
        // GET: Patient/
        public ActionResult Index()
        { 
            return View();
        }

        // GET: Patient/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/PatientData/ListPatients 
            string url = "ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id, FormCollection form)
        {
            Debug.WriteLine("id" + id);
            Debug.WriteLine("form" + form["id"]);
            if (form["id"] != "") 
            { 
                id = Int32.Parse(form["id"]); 
            }
            else
            {
                return View("Error");
            }

            Debug.WriteLine("id" + id);
           
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;

            return View(selectedPatient);
        }

        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string url = "AddPatient";


            string jsonpayload = jss.Serialize(patient);

            HttpContent content = new StringContent(jsonpayload);
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

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedPatient);
        }

        // POST: Patient/Update/5
        [HttpPost]
        public ActionResult Update(int id, Patient patient)
        {
            string url = "UpdatePatient/" + id;
            string jsonpayload = jss.Serialize(patient);
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

        // GET: Patient/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedPatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "DeletePatient/" + id;
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
