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

        /// <summary>
        /// Returns the page where user has to enter patient ID to view the patient details
        /// </summary>
        /// <example>If user enters 3 in the patient ID text box</example>
        /// <returns>It will return the age and gender of the patient ID : 3</returns>

        // GET: Patient/
        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// List all the patients in the database
        /// </summary>
        /// <returns>Returns all the patients data from the database</returns>
        
        // GET: Patient/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/PatientData/ListPatients 
            string url = "ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            return View(patients);
        }

        /// <summary>
        /// Retrieves particular patient details from the database
        /// </summary>
        /// <param name="id">5</param>
        /// <param name="form">All the data entered in the form</param>
        /// <returns>Returns the specific details of the patient</returns>
        
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

        /// <summary>
        /// Create a new patient
        /// </summary>
        /// <returns>Returns a view page to add a new patient</returns>

        // GET: Patient/New
        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Creates a new patient in the database
        /// </summary>
        /// <param name="patient">Patient details entered in the form</param>
        /// <returns>Returns to index page after adding the patient to the database</returns>
        
        // POST: Patient/Create
        [HttpPost]
        [Authorize]
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

        /// <summary>
        /// Returns a view with the details of the selected patient
        /// </summary>
        /// <param name="id">Selected Patient's ID</param>
        /// <returns>Returns a view pre-populated with the selected patient's details</returns>

        // GET: Patient/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedPatient);
        }

        /// <summary>
        /// Updates the modified data of the selected patient in the database
        /// </summary>
        /// <param name="id">Selected Patient's ID</param>
        /// <param name="patient">Patient details</param>
        /// <returns>Returns to the index page</returns>
        
        // POST: Patient/Update/5
        [HttpPost]
        [Authorize]
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

        /// <summary>
        /// Asks user confirmation for deletion
        /// </summary>
        /// <param name="id">Patient ID for deleting their respective details</param>
        /// <returns>Returns a view with the selected patient's details that is selected for deletion</returns>
        
        // GET: Patient/DeleteConfirm/5
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedPatient);
        }

        /// <summary>
        /// Deletes the patient details from the database
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <param name="collection">Patient Details</param>
        /// <returns>Returns to the index page after deleting the patient from database</returns>
        
        // POST: Patient/Delete/5
        [HttpPost]
        [Authorize]
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
