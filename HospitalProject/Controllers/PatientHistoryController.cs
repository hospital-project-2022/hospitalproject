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

        /// <summary>
        /// Retrieves particular patient history details from the database
        /// </summary>
        /// <param name="id">5</param>
        /// <returns>Returns the specific history details of the patient</returns>
        
        // GET: PatientHistory/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {

            string url = "ListPatientHistoryForPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientHistoryDto> selectedPatientHistory = response.Content.ReadAsAsync<IEnumerable<PatientHistoryDto>>().Result;

            return View(selectedPatientHistory);
        }

        /// <summary>
        /// Create a new patient history
        /// </summary>
        /// <returns>Returns a view page to add a new patient history</returns>
        
        // GET: PatientHistory/New
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
        /// Creates a new patient history in the database
        /// </summary>
        /// <param name="patienthistory">Patient history details entered in the form</param>
        /// <returns>Returns to index page after adding the patient history to the database</returns>

        // POST: PatientHistory/Create
        [HttpPost]
        [Authorize]
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

        /// <summary>
        /// Returns a view with the details of the selected patient history
        /// </summary>
        /// <param name="id">Selected Patient History's ID</param>
        /// <returns>Returns a view pre-populated with the selected patient history's details</returns>

        // GET: PatientHistory/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            string url = "FindOnePatientHistory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientHistoryDto selectedPatientHistory = response.Content.ReadAsAsync<PatientHistoryDto>().Result;
            Debug.WriteLine(selectedPatientHistory);
            return View(selectedPatientHistory);
        }

        /// <summary>
        /// Updates the modified data of the selected patient history in the database
        /// </summary>
        /// <param name="id">Selected Patient History's ID</param>
        /// <param name="patienthistory">Patient history details</param>
        /// <returns>Returns to the index page</returns>

        // POST: PatientHistory/Update/5
        [Authorize]
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

        /// <summary>
        /// Asks user confirmation for deletion
        /// </summary>
        /// <param name="id">Patient History ID for deleting their respective details</param>
        /// <returns>Returns a view with the selected patient histoy's details that is selected for deletion</returns>
        
        // GET: PatientHistory/DeleteConfirm/5
        [Authorize]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FindOnePatientHistory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientHistoryDto selectedPatientHistory = response.Content.ReadAsAsync<PatientHistoryDto>().Result;
            return View(selectedPatientHistory);
        }

        /// <summary>
        /// Deletes the patient history details from the database
        /// </summary>
        /// <param name="id">Patient History ID</param>
        /// <param name="collection">Patient History Details</param>
        /// <returns>Returns to the index page after deleting the patient history from database</returns>

        // POST: PatientHistory/Delete/5
        [HttpPost]
        [Authorize]
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
