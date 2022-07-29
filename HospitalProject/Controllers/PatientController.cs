using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient/
        public ActionResult Index()
        { 
            return View();
        }

        // GET: Patient/List
        public ActionResult List()
        {
            //curl https://localhost:44300/api/PatientData/ListPatients 

            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44300/api/PatientData/ListPatients";
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
            HttpClient client = new HttpClient() { };
           
            string url = "https://localhost:44300/api/PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;

            return View(selectedPatient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
